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
    public class ShoppingCartController : Controller
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
    }
}