using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Model
{
    public interface IAddress : IEntity
    {
        string Street { get; set;}
        int Number { get; set; }
    }
}
