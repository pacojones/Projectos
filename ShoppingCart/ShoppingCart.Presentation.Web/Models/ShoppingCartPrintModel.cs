using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace ShoppingCart.Presentation.Web.Models
{
    public class ShoppingCartPrintModel : BaseModel
    {
        public Collection<ShoppingCartItemModel> _items;
    }
}