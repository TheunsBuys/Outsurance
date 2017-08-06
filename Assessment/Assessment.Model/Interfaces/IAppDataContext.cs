using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Model
{
    public interface IAppDataContext
    {
        IRepository<Contact> Contacts { get; set; }
        IRepository<Address> Addresses { get; set; }
    }
}
