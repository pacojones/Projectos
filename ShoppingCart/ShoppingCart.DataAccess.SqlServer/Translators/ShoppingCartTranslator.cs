using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.SqlServer
{
    public class ShoppingCartTranslator : Translator<Business.Entities.ShoppingCart, DataSet>
    {
        public ShoppingCartTranslator(DataSet ds) : base(ds) { }

        public override Business.Entities.ShoppingCart ToBusinessEntity()
        {
            Business.Entities.ShoppingCart shoppingCart = null;

            DataTable shoppingCartTable = this.DataEntity.Tables[0];
            
            if (this.DataEntity.Tables.Count > 0)
            {
                shoppingCart = new Business.Entities.ShoppingCart();

                foreach (DataRow row in shoppingCartTable.Rows)
                {
                    shoppingCart.ID = row.Field<long>("ID");
                    shoppingCart.StatusID = row.Field<long>("StatusID");
                    shoppingCart.OwnerID = row.Field<long>("OwnerID");

                    shoppingCart.State = new Business.Entities.ShoppingCartState();
                    shoppingCart.State.ID = shoppingCart.StatusID;
                    shoppingCart.State.Code = row.Field<string>("StateCode");
                    shoppingCart.State.Description = row.Field<string>("StateDescription");

                    shoppingCart.Owner = new Business.Entities.User();
                    shoppingCart.Owner.ID = row.Field<long>("OwnerID");
                    shoppingCart.Owner.Username = row.Field<string>("OwnerUsername");
                    shoppingCart.Owner.Name = row.Field<string>("OwnerName");
                }

                if (this.DataEntity.Tables.Count > 1)
                {
                    DataTable shoppingCartItemsTable = this.DataEntity.Tables[1];

                    foreach (DataRow row in shoppingCartItemsTable.Rows)
                    {
                        Business.Entities.ShoppingCartItem item = new Business.Entities.ShoppingCartItem();
                        item.ID = row.Field<long>("ID");
                        item.CartID = row.Field<long>("CartID");
                        item.ItemID = row.Field<long>("ItemID");
                        item.Quantity = row.Field<long>("Quantity");
                        item.StoreID = row.Field<long>("StoreID");
                        item.StatusID = row.Field<long>("StatusID");

                        item.Store = new Business.Entities.Store();
                        item.Store.ID = row.Field<long>("StoreID");
                        item.Store.Code = row.Field<string>("StoreCode");
                        item.Store.Description = row.Field<string>("StoreDescription");

                        item.Definition = new Business.Entities.ShoppingCartItemDefinition();
                        item.Definition.DefaultStoreID = row.Field<long>("DefaultStoreID");
                        item.Definition.CategoryID = row.Field<long>("CategoryID");
                        item.Definition.Code = row.Field<string>("DefinitionCode");
                        item.Definition.DefaultQuantity = row.Field<long>("DefaultQuantity");
                        item.Definition.Description = row.Field<string>("DefinitionDescription");
                        item.Definition.ID = row.Field<long>("ItemID");
                        item.Definition.LocationID = row.Field<long>("LocationID");
                        item.Definition.UnitID = row.Field<long>("UnitTypeID");
                        item.Definition.UnitPrice = row.Field<decimal>("UnitPrice");

                        item.Definition.Category = new Business.Entities.Category();
                        item.Definition.Category.ID = row.Field<long>("CategoryID");
                        item.Definition.Category.Code = row.Field<string>("CategoryCode");
                        item.Definition.Category.Description = row.Field<string>("CategoryDescription");

                        item.Definition.Location = new Business.Entities.Location();
                        item.Definition.Location.ID = row.Field<long>("LocationID");
                        item.Definition.Location.Code = row.Field<string>("LocationCode");
                        item.Definition.Location.Description = row.Field<string>("LocationDescription");
                        item.Definition.Location.Order = row.Field<long>("LocationOrder");

                        item.Definition.DefaultStore = new Business.Entities.Store();
                        item.Definition.DefaultStore.ID = row.Field<long>("DefaultStoreID");
                        item.Definition.DefaultStore.Code = row.Field<string>("DefaultStoreCode");
                        item.Definition.DefaultStore.Description = row.Field<string>("DefaultStoreDescription");

                        item.Definition.Unit = new Business.Entities.UnitType();
                        item.Definition.Unit.ID = row.Field<long>("UnitTypeID");
                        item.Definition.Unit.Code = row.Field<string>("UnitCode");
                        item.Definition.Unit.Description = row.Field<string>("UnitDescription");

                        item.State = new Business.Entities.ShoppingCartItemState();
                        item.State.ID = row.Field<long>("StatusID");
                        item.State.Code = row.Field<string>("StatusCode");
                        item.State.Description = row.Field<string>("StatusDescription");

                        shoppingCart.Items.Add(item);
                    }
                }
            }
            
            return shoppingCart;
        }
    }
}
