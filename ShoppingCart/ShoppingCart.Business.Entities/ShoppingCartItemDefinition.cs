using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Business.Entities
{
    public class ShoppingCartItemDefinition : BusinessDescriptionEntity
    {
        public long CategoryID { get; set; }

        public long UnitID { get; set; }

        public long LocationID { get; set; }

        public decimal UnitPrice { get; set; }

        public long DefaultStoreID { get; set; }

        public long DefaultQuantity { get; set; }

        public bool Enabled { get; set; }

        public Category Category { get; set; }

        public UnitType Unit { get; set; }

        public Location Location { get; set; }

        public Store DefaultStore { get; set; }
    }
}
