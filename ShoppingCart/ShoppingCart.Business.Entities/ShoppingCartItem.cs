﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShoppingCart.Business.Entities
{
    [XmlRoot("Item")]
    public class ShoppingCartItem : BusinessEntity
    {
        public long CartID { get; set; }
        public long ItemID {get;set;}

        public long Quantity { get; set; }

        public long StoreID { get; set; }

        public long StatusID { get; set; }

        public ShoppingCartItemDefinition Definition { get; set; }

        public ShoppingCartItemState State { get; set; }

        public Store Store { get; set; }
    }
}
