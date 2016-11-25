using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShoppingCart.Business.Entities
{
    [XmlRoot("ShoppingCartItems")]
    public class ShoppingCartItemCollection : Collection<ShoppingCartItem>
    {
    }
}
