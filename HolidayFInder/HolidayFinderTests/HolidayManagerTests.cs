using HolidayFinder;
using HolidayFinder.FileReader;
using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HolidayFinder.Models;
using System.IO;
using Moq;
using Newtonsoft.Json;

namespace HolidayFinderTests
{
    public class HolidayManagerTests
    {
        private HolidayManager _holidayManager;
        private Mock<IReader> _fileReader;

        [SetUp]
        public void Setup()
        {
            _fileReader = new Mock<IReader>();
            _holidayManager = new HolidayManager(_fileReader.Object);
        }

        [Test]
        public void SearchHoliday_ShouldReturnHolidaySearchResult()
        {
            //Arrange
            var flightData = GetFlightData();
            var hotelData = GetHotelData();
            var airportData = GetAirportData();
            _fileReader.Setup(x => x.ReadFile<Flight>(It.IsAny<string>())).Returns(flightData);
            _fileReader.Setup(x => x.ReadFile<Hotel>(It.IsAny<string>())).Returns(hotelData);
            _fileReader.Setup(x => x.ReadFile<Airport>(It.IsAny<string>())).Returns(airportData);

            //Act
            var result = _holidayManager.SearchHoliday(departingFrom: "TFS", travellingTo: "AGP",
                                                        departureDate: "2023/07/01", duration: 7);

            //Assert
            result.Should().BeOfType<HolidaySearchResult>();
        }

        [Test]
        public void Given_MAN_To_AGP_On_01_07_2023_For7Nights_ShouldReturnFlight2Hotel9()
        {
            //Arrange
            var flightData = GetFlightData();
            var hotelData = GetHotelData();
            var airportData = GetAirportData();
            _fileReader.Setup(x => x.ReadFile<Flight>(It.IsAny<string>())).Returns(flightData);
            _fileReader.Setup(x => x.ReadFile<Hotel>(It.IsAny<string>())).Returns(hotelData);
            _fileReader.Setup(x => x.ReadFile<Airport>(It.IsAny<string>())).Returns(airportData);

            //Act
            var holidays = _holidayManager.SearchHoliday(departingFrom: "MAN",  travellingTo: "AGP", 
                                                        departureDate: "2023/07/01", duration: 7);

            //Assert
            holidays.Results.First().Flight.Id.Should().Be(2);
            holidays.Results.First().Hotel.Id.Should().Be(9);
        }

        [Test]
        public void Given_AnyAirport_To_LPA_On_10_11_2022_For14Nights_ShouldReturnFlight7Hotel6()
        {
            //Arrange
            var flightData = GetFlightData();
            var hotelData = GetHotelData();
            var airportData = GetAirportData();
            _fileReader.Setup(x => x.ReadFile<Flight>(It.IsAny<string>())).Returns(flightData);
            _fileReader.Setup(x => x.ReadFile<Hotel>(It.IsAny<string>())).Returns(hotelData);
            _fileReader.Setup(x => x.ReadFile<Airport>(It.IsAny<string>())).Returns(airportData);

            //Act
            var holidays = _holidayManager.SearchHoliday(travellingTo: "LPA",
                                                        departureDate: "2022/11/10", duration: 14);

            //Assert
            holidays.Results.First().Flight.Id.Should().Be(7);
            holidays.Results.First().Hotel.Id.Should().Be(6);
        }

        [Test]
        public void Given_AnyLondonAirport_To_PMI_On_15_06_2023_For10Nights_ShouldReturnFlight6Hotel5()
        {
            //Arrange
            var flightData = GetFlightData();
            var hotelData = GetHotelData();
            var airportData = GetAirportData();
            _fileReader.Setup(x => x.ReadFile<Flight>(It.IsAny<string>())).Returns(flightData);
            _fileReader.Setup(x => x.ReadFile<Hotel>(It.IsAny<string>())).Returns(hotelData);
            _fileReader.Setup(x => x.ReadFile<Airport>(It.IsAny<string>())).Returns(airportData);

            //Act
            var holidays = _holidayManager.SearchHoliday(departingFrom: "London", travellingTo: "PMI",
                                                        departureDate: "2023/06/15", duration: 10);

            //Assert
            holidays.Results.First().Flight.Id.Should().Be(6);
            holidays.Results.First().Hotel.Id.Should().Be(5);
        }

        [Test]
        public void Given_MAN_To_AnySpainAirport_On_15_06_2023_For10Nights_ShouldReturnHolidaysInSpain()
        {
            //Arrange
            var flightData = GetFlightData();
            var hotelData = GetHotelData();
            var airportData = GetAirportData();
            _fileReader.Setup(x => x.ReadFile<Flight>(It.IsAny<string>())).Returns(flightData);
            _fileReader.Setup(x => x.ReadFile<Hotel>(It.IsAny<string>())).Returns(hotelData);
            _fileReader.Setup(x => x.ReadFile<Airport>(It.IsAny<string>())).Returns(airportData);
            var spainAirports = new List<string>() { "TFS","AGP","PMI","LPA"};

            //Act
            var holidays = _holidayManager.SearchHoliday(departingFrom: "MAN", travellingTo: "Spain",
                                                        departureDate: "2023/06/15", duration: 10);

            //Assert
            holidays.Results.Any().Should().BeTrue();
            holidays.Results.All(h => spainAirports.Contains(h.Flight.To));
        }

        #region Test Data Setup

        private List<Flight> GetFlightData()
        {
            string filePath = $"{Environment.CurrentDirectory}\\InputTestData\\FlightData.json";
            var JsonTxt = File.ReadAllText(filePath);
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                DateFormatString = "yyyy-MM-dd"
            };
            return JsonConvert.DeserializeObject<List<Flight>>(JsonTxt, settings);
        }
        
        private List<Hotel> GetHotelData()
        {
            string filePath = $"{Environment.CurrentDirectory}\\InputTestData\\HotelData.json";
            var JsonTxt = File.ReadAllText(filePath);
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                DateFormatString = "yyyy-MM-dd"
            };
            return JsonConvert.DeserializeObject<List<Hotel>>(JsonTxt, settings);
        }

        private List<Airport> GetAirportData()
        {
            string filePath = $"{Environment.CurrentDirectory}\\InputTestData\\AirportData.json";
            var JsonTxt = File.ReadAllText(filePath);
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                DateFormatString = "yyyy-MM-dd"
            };
            return JsonConvert.DeserializeObject<List<Airport>>(JsonTxt, settings);
        }

        #endregion
    }
}
