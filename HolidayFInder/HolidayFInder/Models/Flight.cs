using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HolidayFinder.Models
{
    public class Flight : IReadable
    {
        public int Id { get; set; }
        public string Airline { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public float Price { get; set; }
        [JsonProperty("departure_date")]
        public DateTime DepartureDate { get; set; }
    }
}
