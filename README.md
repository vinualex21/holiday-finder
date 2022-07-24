# Holiday Finder
A search library that returns the best value holidays. The program reads two JSON files containing flight and hotel data and filters it based on customer requirements. A third JSON file containing codes of airports and the mapping to their name and city provides partial search feature.

## Search Method
  

- ### HolidayManager.SearchHoliday()
  
  Based on data from the two JSON files, the method finds flight-hotel pairs that match the parameters provided.

 
#### Parameters
  
| Name  | Type | Default Value | Mandatory | Example  | Description |
| ------------- | ------------- | ------------- | ------------- | ------------- | ------------- |
| departingFrom  | string  | null | No | LGW  | Departure airport code, name, city or country  |
| travellingTo  | string  | null | No | PMI  | Arrival airport code, name, city or country  |
| departureDate  | string  | null | No | 2023-06-15  | Date of departure  |
| duration  | int  | 7 | No | 10  | Number of nights  |

## Assumptions
The departure date, arrival date and the start date of the hotel stay are all the same date. 

## JSON Data files
The [InputData](https://github.com/vinualex21/holiday-finder/tree/main/HolidayFInder/HolidayFInder/InputData) folder contain three JSON files.

 - ### FlightData.json
 It contains a collection of flight data with their id, airline name, departure and arrival airport codes, price and departure date.
 ```
 [
 {
    "id": 11,
    "airline": "First Class Air",
    "from": "LGW",
    "to": "AGP",
    "price": 155,
    "departure_date": "2023-07-01"
  }
  ]
  ```
  
  - ### HotelData.json
 It contains a collection of hotel data with their id, name, arrival date, price per night, a collection of local airports and the number of nights booked.
 ```
 [
 {
		"id": 10,
		"name": "Barcelo Malaga",
		"arrival_date": "2023-07-05",
		"price_per_night": 45,
		"local_airports": [ "AGP" ],
		"nights": 10
  }
  ]
  ```
  
  - ### AirportData.json
 It contains a collection of airports with their IATA code, name, city and country.
 ```
 [
 {
		"code": "TFS",
		"name": "Tenerife",
		"city": "Canary Islands",
		"country": "Spain"
	}
  ]
  ```
  
  ## How to use the library
  - Build the solution.
  - Copy the HolidayFinder.dll file into the bin folder of your project.
  - Add the project reference in the project.
  - Create a folder named InputData in the working directory and place the JSON files in it.
  - Create an object of HolidayManager and invoke the SearchHoliday() method with the required parameters.
  
