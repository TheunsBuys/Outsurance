using Assessment.Model;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.DataLayer.Mappings
{
    public class AddressMap: CsvClassMap<Address>
    {
        public AddressMap()
        {
            Map(x => x.Id)
                .Ignore();
            Map(x => x.Number)
                .ConvertUsing(x => int.Parse(x.GetField(2).Split(null).First()));
            Map(x=>x.Street)
                .ConvertUsing(x => string.Join(" ", x.GetField(2).Split(null).Skip(1)));
        }
    }
}
