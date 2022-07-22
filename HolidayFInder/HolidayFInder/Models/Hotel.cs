using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayFinder.Models
{
    public class Hotel : IReadable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("arrival_date")]
        public DateTime ArrivalDate { get; set; }
        [JsonProperty("price_per_night")]
        public float PricePerNight { get; set; }
        [JsonProperty("local_airports")]
        public List<string> LocalAirports { get; set; }
        [JsonProperty("nights")]
        public int NumberOfNights { get; set; }
    }
}
