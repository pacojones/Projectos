using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart.Business.Core;
using ShoppingCart.Common;

namespace ShoppingCart.UnitTests
{
    [TestClass]
    public class BusinessTests
    {
        private IShoppingCartManager manager;

        private DataContext context;

        [TestInitialize]
        public void Initialize()
        {
            this.context = new DataContext(0, "EUR", "pt-PT", "admin");

            this.manager = new ShoppingCartManager(this.context);
        }

        [TestMethod]
        public void Business_ShoppingCartManager_Add()
        {
            this.manager.Insert();
        }

        [TestMethod]
        public void Business_ShoppingCartManager_Select()
        {
            var shoppingCart = this.manager.Select(1);

            Assert.IsNotNull(shoppingCart);
            Assert.IsNotNull(shoppingCart.Items);
            Assert.IsTrue(shoppingCart.Items.Count > 0);
        }

        [TestMethod]
        public void Business_Test_Serializar()
        {

        }
    }
}
