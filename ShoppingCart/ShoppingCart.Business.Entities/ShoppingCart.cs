using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShoppingCart.Business.Entities
{
    public class ShoppingCart : BusinessEntity
    {
        private ShoppingCartItemCollection _items;

        public long OwnerID { get; set; }

        public long StatusID { get; set; }

        public ShoppingCartState State {get;set;}

        public User Owner { get; set; }

        public ShoppingCartItemCollection Items
        {
            get
            {
                if (_items == null)
                    _items = new ShoppingCartItemCollection();

                return _items;
            }
        }
    }
}
