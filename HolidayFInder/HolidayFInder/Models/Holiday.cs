using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayFinder.Models
{
    public class Holiday
    {
        public float TotalPrice { get; set; }
        public Flight Flight { get; set; }
        public Hotel Hotel { get; set; }
    }
}
