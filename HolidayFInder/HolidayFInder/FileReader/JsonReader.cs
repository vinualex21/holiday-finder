using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HolidayFinder.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace HolidayFinder.FileReader
{
    public class JsonReader : IReader
    {
        public List<T> ReadFile<T>(string filePath) where T : IReadable
        {
            List<T> result = new List<T>();
            
            if (!File.Exists(filePath))
                throw new FileNotFoundException();
            try
            {
                var JsonTxt = File.ReadAllText(filePath);
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    DateFormatString = Constants.JSON_DATE_FORMAT
                };
                result = JsonConvert.DeserializeObject<List<T>>(JsonTxt, settings);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
    }
}
