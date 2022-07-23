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
            _fileReader.Setup(x => x.ReadFile<Flight>(It.IsAny<string>())).Returns(flightData);
            _fileReader.Setup(x => x.ReadFile<Hotel>(It.IsAny<string>())).Returns(hotelData);

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
            _fileReader.Setup(x=>x.ReadFile<Flight>(It.IsAny<string>())).Returns(flightData);
            _fileReader.Setup(x=>x.ReadFile<Hotel>(It.IsAny<string>())).Returns(hotelData);

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
            _fileReader.Setup(x => x.ReadFile<Flight>(It.IsAny<string>())).Returns(flightData);
            _fileReader.Setup(x => x.ReadFile<Hotel>(It.IsAny<string>())).Returns(hotelData);

            //Act
            var holidays = _holidayManager.SearchHoliday(travellingTo: "LPA",
                                                        departureDate: "2022/11/10", duration: 14);

            //Assert
            holidays.Results.First().Flight.Id.Should().Be(7);
            holidays.Results.First().Hotel.Id.Should().Be(6);
        }

        #region Data Setup

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

        #endregion
    }
}
