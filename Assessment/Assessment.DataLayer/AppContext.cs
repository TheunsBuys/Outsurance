using Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.DataLayer
{
    public class AppDataContext : IAppDataContext
    {
        public AppDataContext()
        {
            Contacts = new Repository<Contact>();
            Addresses = new Repository<Address>();
        }

        public AppDataContext(string filePath)
        {
            Contacts = new CsvRepository<Contact>(filePath);
            Addresses = new CsvRepository<Address>(filePath);
        }

        public virtual IRepository<Contact> Contacts { get; set; }
        public virtual IRepository<Address> Addresses { get; set; }

        public void Dispose()
        {
            Contacts?.Dispose();
            Addresses?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
