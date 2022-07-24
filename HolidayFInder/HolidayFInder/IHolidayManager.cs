using HolidayFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayFinder
{
    public interface IHolidayManager
    {
        public HolidaySearchResult SearchHoliday(
                                string departingFrom = null,
                                string travellingTo = null,
                                string departureDate = null,
                                int duration = 0);
    }
}
