using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Assessment.DataLayer.Mappings;

namespace Assessment.DataLayer.Parsers
{
    public static class CSVParser
    {
        public static IEnumerable<T> Load<T>(string path)
        {
            if (File.Exists(path))
            {
                using (var reader = new StringReader(File.ReadAllText(path)))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap(typeof(ContactMap));
                    csv.Configuration.RegisterClassMap(typeof(AddressMap));
                    return csv.GetRecords<T>()
                              .ToList();
                }
            }
            return new List<T>();
        }
    }
}
