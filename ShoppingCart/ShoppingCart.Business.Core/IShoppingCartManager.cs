using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Business.Entities;
using System.Collections.ObjectModel;

namespace ShoppingCart.Business.Core
{
    public interface IShoppingCartManager
    {
        long Insert();

        void Update(long id, long actionId);

        void ItemUpdate(long itemId, long quantity, long storeId);

        Entities.ShoppingCart Select(long id);

        Collection<Entities.ShoppingCart> SelectShoppingCarts();
    }
}
