using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Model.Domain
{
    public class ContactDomain
    {
        public bool ImportFromFile (IAppDataContext context, string filePath)
        {
            return false;
        }

        public IEnumerable<KeyValuePair<int, Contact>> SortByNamesAndFrequency(IAppDataContext context)
        {
            return context.Contacts.GroupBy(x => x.LastName)
                                   .Select(x => new { count = x.Count(), contact = x.First() })
                                   .OrderByDescending(x => x.count)
                                   .ThenBy(x => x.contact.LastName)
                                   .Select(x => new KeyValuePair<int, Contact>(x.count, x.contact));
        }

        public IEnumerable<KeyValuePair<int, string>> GetSortedListByNamesAndFrequency(IAppDataContext context)
        {
            var names = new List<string>();
            names.AddRange(context.Contacts.Select(x => x.FirstName));
            names.AddRange(context.Contacts.Select(x => x.LastName));
            return names.GroupBy(x => x)
                        .Select(x => new { count = x.Count(), name = x.First() })
                        .OrderByDescending(x => x.count)
                        .ThenBy(x => x.name)
                        .Select(x => new KeyValuePair<int, string>(x.count, x.name));
        }

        public bool ExportToFile (IEnumerable<KeyValuePair<int, string>> sortedContacts, string filePath )
        {
            try
            {
                using (var sw = new StreamWriter(filePath, false))
                {
                    foreach (var contact in sortedContacts)
                    {
                        sw.WriteLine($"{contact.Key} {contact.Value}");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
