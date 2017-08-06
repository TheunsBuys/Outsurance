using Assessment.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Domain
{
    public class AddressDomain
    {
        public IEnumerable<string> SortByStreetAndFrequency(IAppDataContext context)
        {
            return context.Addresses.OrderBy (x => x.Street)
                                    .Select(x => $"{x.Number} {x.Street}");
        }

        public bool ExportToFile(IEnumerable<string> sortedAddresses, string filePath)
        {
            try
            {
                File.WriteAllLines(filePath, sortedAddresses);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
