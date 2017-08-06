using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assessment.DataLayer;
using Assessment.Model;
using Assessment.DataLayer.Parsers;
using Assessment.Model.Domain;

namespace Assessment.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //IEnumerable<Contact> contacts = CSVParser.Load<Contact>(@"C:\Users\Theuns\Documents\Outsurance\data.csv");

            var contactDomain = new ContactDomain();
            var context = new AppDataContext(@"C:\Users\Theuns\Documents\Outsurance\data.csv");
            contactDomain.GetSortedListByNamesAndFrequency(context);

        }
    }
}
