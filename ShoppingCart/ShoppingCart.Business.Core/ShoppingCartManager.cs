using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Business.Entities;
using ShoppingCart.DataAccess.SqlServer;
using ShoppingCart.Common;
using ShoppingCart.DataAccess;
using System.Collections.ObjectModel;

namespace ShoppingCart.Business.Core
{
    public class ShoppingCartManager : BaseManager, IShoppingCartManager
    {
        private IDataMapper<Entities.ShoppingCart> shoppingCartDataMapper;

        private IDataMapper<Entities.ShoppingCartItem> shoppingCartItemDataMapper;

        public ShoppingCartManager (DataContext context) : base(context)
        {
            this.shoppingCartDataMapper = new ShoppingCartDataMapper(context);
            this.shoppingCartItemDataMapper = new ShoppingCartItemDataMapper(context);
        }

        public long Insert()
        {
            return this.shoppingCartDataMapper.Add(null);
        }

        public void ItemUpdate(long itemId, long quantity, long storeId)
        {
            Entities.ShoppingCartItem item = new ShoppingCartItem();
            item.ID = itemId;
            item.Quantity = quantity;
            item.StoreID = storeId;

            this.shoppingCartItemDataMapper.Update(item);
        }

        public Entities.ShoppingCart Select(long id)
        {
            return this.shoppingCartDataMapper.GetById(id);
        }

        public Collection<Entities.ShoppingCart> SelectShoppingCarts()
        {
            return this.shoppingCartDataMapper.GetByCriteria(new ShoppingCartListSearchCriteria());
        }

        public void Update(long id, long actionId)
        {
            Entities.ShoppingCart cart = new Entities.ShoppingCart();
            cart.ID = id;

            ShoppingCartUpdateCriteria criteria = new ShoppingCartUpdateCriteria();
            criteria.ActionID = actionId;

            this.shoppingCartDataMapper.Update(cart, criteria);
        }
    }
}
