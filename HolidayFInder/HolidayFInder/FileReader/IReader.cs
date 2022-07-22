using HolidayFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayFinder.FileReader
{
    public interface IReader
    {
        public List<T> ReadFile<T>(string filePath) where T : IReadable;
    }
}
