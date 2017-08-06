using Assessment.Model;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.DataLayer.Mappings
{
    public class ContactMap : CsvClassMap<Contact>
    {
        public ContactMap()
        {
            Map(x => x.Id)
                .Ignore();
            Map(x => x.FirstName)
                .Name("FirstName");
            Map(x => x.LastName)
                .Name("LastName");
            Map(x => x.PhoneNumber)
                .Name("PhoneNumber");
            References<AddressMap>(x => x.Address);
        } 
    }
}
