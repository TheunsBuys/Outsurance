using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Model
{
    public class Address : IAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }
}
