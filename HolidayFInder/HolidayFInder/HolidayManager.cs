﻿using HolidayFinder.FileReader;
using HolidayFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayFinder
{
    public class HolidayManager : IHolidayManager
    {
        private IReader _fileReader;

        string flightFilePath = $"{Environment.CurrentDirectory}{Constants.FLIGHT_FILE_PATH}";
        string hotelFilePath = $"{Environment.CurrentDirectory}{Constants.HOTEL_FILE_PATH}";
        string airportFilePath = $"{Environment.CurrentDirectory}{Constants.AIRPORT_FILE_PATH}";

        public HolidayManager(IReader fileReader)
        {
            _fileReader = fileReader;
        }

        public HolidaySearchResult SearchHoliday(
                                string departingFrom = null, 
                                string travellingTo = null, 
                                string departureDate = null, 
                                int duration = 0)
        {
            var flights = _fileReader.ReadFile<Flight>(flightFilePath);
            var hotels = _fileReader.ReadFile<Hotel>(hotelFilePath);

            if(duration == 0)
                duration = Constants.DEFAULT_HOTEL_STAY_DURATION;

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
            {
                var departureAirports = GetAirportCodes(departingFrom);
                query = query.Where(x => departureAirports.Contains(x.Flight.From));
            }

            if (travellingTo != null)
            {
                var arrivalAirports = GetAirportCodes(travellingTo);
                query = query.Where(x => arrivalAirports.Contains(x.Flight.To));
            }

            if (departureDate != null)
            {
                if (DateTime.TryParse(departureDate, out DateTime depDate))
                    query = query.Where(x => x.Flight.DepartureDate == depDate);
                else
                    throw new FormatException("Invalid departure date. Please enter in the format dd/mm/yyyy or yyyy/mm/dd");
            }

            query = query.Where(x => x.Hotel.NumberOfNights == duration);

            var holidays = query.Select(h =>
                new Holiday()
                {
                    TotalPrice = h.Flight.Price + h.Hotel.PricePerNight * duration,
                    Flight = h.Flight,
                    Hotel = h.Hotel
                })
                .OrderBy(h => h.TotalPrice)
                .ToList();
             
            return new HolidaySearchResult() { Results = holidays };
        }

        private List<string> GetAirportCodes(string searchInput)
        {
            var airports = _fileReader.ReadFile<Airport>(airportFilePath);
            IEnumerable<Airport> airportMatch = null;
            airportMatch = airports.Where(a => a.Code == searchInput);
            if (airportMatch?.Count() == 0)
            {
                airportMatch = airports.Where(a => a.City.Contains(searchInput));
            }
            if (airportMatch?.Count() == 0)
            {
                airportMatch = airports.Where(a => a.Name.Contains(searchInput));
            }
            if (airportMatch?.Count() == 0)
            {
                airportMatch = airports.Where(a => a.Country.Contains(searchInput));
            }
            return airportMatch?.Select(a => a.Code).ToList();
        }
    }
}
