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

namespace HolidayFinderTests.FileReaderTests
{
    public class JsonReaderTests
    {
        private JsonReader _jsonReader;

        [SetUp]
        public void Setup()
        {
            _jsonReader = new JsonReader();
        }

        [Test]
        public void GivenInvalidFilePath_ShouldThrowFileNotFoundException()
        {
            //Arrange
            string filePath = @"\invalidPath";

            //Assert
            _jsonReader.Invoking(y => y.ReadFile<Flight>(filePath))
                                .Should().Throw<FileNotFoundException>();
        } 

        [Test]
        public void GivenFlightJsonFile_ShouldReturnListOfFlights()
        {
            //Arrange
            string filePath = $"{Environment.CurrentDirectory}\\InputTestData\\FlightData.json";

            //Act
            var results = _jsonReader.ReadFile<Flight>(filePath);

            //Assert
            results.Should().BeOfType(typeof(List<Flight>));
        }

        [Test]
        public void GivenFlightData_ShouldReturnCorrectData()
        {
            //Arrange
            string filePath = $"{Environment.CurrentDirectory}\\InputTestData\\FlightData.json";
            var expectedResult = new Flight()
            {
                Id = 5,
                Airline = "Fresh Airways",
                From = "MAN",
                To = "PMI",
                Price = 130,
                DepartureDate = new DateTime(2023, 6, 15)
            };

            //Act
            var results = _jsonReader.ReadFile<Flight>(filePath);
            var actualresult = results.Single(x => x.Id == expectedResult.Id);

            //Assert
            actualresult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void GivenHotelJsonFile_ShouldReturnListOfHotels()
        {
            //Arrange
            string filePath = $"{Environment.CurrentDirectory}\\InputTestData\\HotelData.json";

            //Act
            var results = _jsonReader.ReadFile<Hotel>(filePath);

            //Assert
            results.Should().BeOfType(typeof(List<Hotel>));
        }

        [Test]
        public void GivenHotelData_ShouldReturnCorrectData()
        {
            //Arrange
            string filePath = $"{Environment.CurrentDirectory}\\InputTestData\\HotelData.json";
            var expectedResult = new Hotel()
            {
                Id = 11,
                Name = "Parador De Malaga Gibralfaro",
                ArrivalDate = new DateTime(2023, 10, 16),
                PricePerNight = 100,
                LocalAirports = new List<string>() { "AGP"},
                NumberOfNights = 7
                
            };

            //Act
            var results = _jsonReader.ReadFile<Hotel>(filePath);
            var actualresult = results.Single(x => x.Id == expectedResult.Id);

            //Assert
            actualresult.Should().BeEquivalentTo(expectedResult);
        }
    }
}
