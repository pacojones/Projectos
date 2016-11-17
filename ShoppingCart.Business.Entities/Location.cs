using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Business.Entities
{
    public class Location : BusinessDescriptionEntity
    {
        public long Order { get; set; }
    }
}
