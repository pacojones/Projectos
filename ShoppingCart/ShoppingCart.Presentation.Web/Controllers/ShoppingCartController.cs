using ShoppingCart.Business.Core;
using ShoppingCart.Presentation.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Presentation.Web.Controllers
{
    public class ShoppingCartController : BaseController
    {
        IShoppingCartManager shoppingCartManager;

        IStoreManager storeManager;

        public ShoppingCartController()
        {
            shoppingCartManager = new ShoppingCartManager(SessionData.Context);
            storeManager = new StoreManager(SessionData.Context);
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var shoppingCarts = shoppingCartManager.SelectShoppingCarts();

            ShoppingCartListModel model = ShoppingCartListModel.FromBusinessEntity(shoppingCarts);

            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            long cartID = shoppingCartManager.Insert();

            return RedirectToAction("Select", new { id = cartID });
        }

        public async Task<ActionResult> Select(long? id)
        {
            var stores = storeManager.SelectStores();

            var shoppingCart = shoppingCartManager.Select(id.Value);

            ShoppingCartModel model = ShoppingCartModel.FromBusinessEntity(shoppingCart);
            model.Stores = StoreModel.FromBusinessEntityCollection(stores);

            return View(model);
        }

        public async Task<ActionResult> Print(long? id)
        {
            var stores = storeManager.SelectStores();

            var shoppingCart = shoppingCartManager.Select(id.Value);

            ShoppingCartModel model = ShoppingCartModel.FromBusinessEntity(shoppingCart);
            model.Stores = StoreModel.FromBusinessEntityCollection(stores);

            return PartialView(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Update(FormCollection action, string save, string finalize)
        {
            string cartId = action["Id"].ToString();

            try
            {
                Business.Entities.ShoppingCart cart = new Business.Entities.ShoppingCart();

                long actionId = save == "SAVE" ? 4 : 1;
                
                string[] iDs = action["hfID"].Split(',');
                string[] quantity = action["txtQuantity"].Split(',');
                string[] storeId = action["ddlStores"].Split(',');

                for(int i = 0; i< iDs.Length; i++)
                {
                    Business.Entities.ShoppingCartItem item = new Business.Entities.ShoppingCartItem();
                    item.ID = long.Parse(iDs[i]);
                    item.Quantity = long.Parse(quantity[i]);
                    item.StoreID = long.Parse(storeId[i]);

                    cart.Items.Add(item);
                }

                cart.ID = long.Parse(cartId);

                shoppingCartManager.Update(cart, actionId);

                return RedirectToAction("Select", new { id = cartId });
            }
            catch(Exception ex)
            {
                Danger(ex.Message);
                return RedirectToAction("Select", new { id = cartId });
            }
        }

    }
}