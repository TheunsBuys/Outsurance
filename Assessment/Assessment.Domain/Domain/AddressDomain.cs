using Assessment.Model;
using System;
using System.Collections.Generic;
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
    }
}
