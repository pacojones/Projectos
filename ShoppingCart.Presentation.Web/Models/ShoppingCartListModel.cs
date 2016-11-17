using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace ShoppingCart.Presentation.Web.Models
{
    public class ShoppingCartListModel
    {
        private Collection<ShoppingCartModel> shoppingCarts;
        private long openShoppingCartID;

        public Collection<ShoppingCartModel> ShoppingCarts
        {
            get
            {
                if (this.shoppingCarts == null)
                    this.shoppingCarts = new Collection<ShoppingCartModel>();

                return shoppingCarts;
            }
            set
            {
                this.shoppingCarts = value;

                if (value.Any(sc => sc.State.Code == "OPEN"))
                {
                    this.openShoppingCartID = value.First(sc => sc.State.Code == "OPEN").Id;
                }
            }
        }

        public long? OpenShoppingCartID
        {
            get
            {
                if (this.shoppingCarts.Any(sc => sc.State.Code == "OPEN"))
                {
                    return this.shoppingCarts.First(sc => sc.State.Code == "OPEN").Id;
                }

                return null;
            }
        }

        public static ShoppingCartListModel FromBusinessEntity(Collection<Business.Entities.ShoppingCart> shoppingCarts)
        {
            ShoppingCartListModel model = new Models.ShoppingCartListModel();

            foreach(var shoppingCart in shoppingCarts)
            {
                ShoppingCartModel cartModel = ShoppingCartModel.FromBusinessEntity(shoppingCart);
                model.ShoppingCarts.Add(cartModel);
            }

            return model;
        }

    }
}