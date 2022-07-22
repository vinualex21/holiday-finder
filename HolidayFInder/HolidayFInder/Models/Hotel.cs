using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayFinder.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ArrivalDate { get; set; }
        public float PricePerNight { get; set; }
        public string[] LocalAirports { get; set; }
        public int NumberOfNights { get; set; }
    }
}
