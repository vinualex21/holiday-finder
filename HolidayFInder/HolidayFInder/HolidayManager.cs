using HolidayFinder.FileReader;
using HolidayFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayFinder
{
    public class HolidayManager
    {
        private const int DEFAULT_DURATION = 7;
        private IReader _fileReader;
        string flightFilePath = $"{Environment.CurrentDirectory}\\InputData\\FlightData.json";
        string hotelFilePath = $"{Environment.CurrentDirectory}\\InputData\\HotelData.json";

        public HolidayManager(IReader fileReader)
        {
            _fileReader = fileReader;
        }

        public HolidaySearchResult SearchHoliday(
                                string departingFrom = null, 
                                string travellingTo = null, 
                                string departureDate = null, 
                                int duration = DEFAULT_DURATION)
        {
            var flights = _fileReader.ReadFile<Flight>(flightFilePath);
            var hotels = _fileReader.ReadFile<Hotel>(hotelFilePath);

            var query = from flight in flights
                        join hotel in hotels
                            on flight.DepartureDate.Date equals hotel.ArrivalDate.Date
                        where hotel.LocalAirports.Contains(flight.To)
                        select new
                        {
                            Flight = flight,
                            Hotel = hotel
                        };

            if (departingFrom != null)
                query = query.Where(x => x.Flight.From == departingFrom);

            if (travellingTo != null)
                query = query.Where(x => x.Flight.To == travellingTo);

            if(departureDate != null)
            {
                if (DateTime.TryParse(departureDate, out DateTime depDate))
                    query = query.Where(x => x.Flight.DepartureDate == depDate);
                else
                    throw new FormatException("Invalid departure date. Please enter in the format yyyy/mm/dd");
            }

            query = query.Where(x => x.Hotel.NumberOfNights == duration);
            query = query.OrderBy(x => x.Hotel.PricePerNight);

            var temp = query.ToList();
            var holidays = query.Select(h=>
                new Holiday()
                {
                    TotalPrice = h.Flight.Price + h.Hotel.PricePerNight*duration,
                    Flight = h.Flight,
                    Hotel = h.Hotel
                }).ToList();
             
            return new HolidaySearchResult() { Results = holidays };
        }
    }
}
