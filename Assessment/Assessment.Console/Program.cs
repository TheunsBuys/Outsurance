using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assessment.DataLayer;
using Assessment.Model;
using Assessment.DataLayer.Parsers;
using Assessment.Model.Domain;
using Assessment.Domain;
using System.IO;

namespace Assessment.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x));
            System.Console.WriteLine($"Processing file {path}");
            if (File.Exists(path))
            {
                using (var context = new AppDataContext(path))
                {
                    var contactDomain = new ContactDomain();
                    var addressDomain = new AddressDomain();
                    IEnumerable<KeyValuePair<int, string>> contacts = contactDomain.GetSortedListByNamesAndFrequency(context);
                    IEnumerable<string> addresses = addressDomain.SortByStreetAndFrequency(context);

                    System.Console.WriteLine($"{contacts.Count()} Names extracted");
                    System.Console.WriteLine($"{addresses.Count()} Addresses extracted");
                    System.Console.WriteLine($"Exporting to {Path.GetDirectoryName(path)}");

                    contactDomain.ExportToFile(contacts, Path.Combine(Path.GetFullPath(path), "contacts.txt" ));
                    addressDomain.ExportToFile(addresses, Path.Combine(Path.GetFullPath(path), "addresses.txt"));
                    System.Console.WriteLine($"Exporting done, press any key");
                    System.Console.ReadKey();

                }
            }    
            
        }
    }
}
