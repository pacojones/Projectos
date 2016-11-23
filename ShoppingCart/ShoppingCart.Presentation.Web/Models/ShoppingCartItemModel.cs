using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Presentation.Web.Models
{
    public class ShoppingCartItemModel : BaseModel
    {
        public long ItemID { get; set; }

        public decimal Quantity { get; set; }

        public long StoreID { get; set; }

        public long StatusID { get; set; }

        public ShoppingCartItemDefinitionModel Definition { get; set; }

        public ShoppingCartItemStateModel State { get; set; }

        public StoreModel Store { get; set; }

        public static ShoppingCartItemModel FromBusinessEntity(Business.Entities.ShoppingCartItem businessEntity)
        {
            ShoppingCartItemModel model = new ShoppingCartItemModel();
            model.Id = businessEntity.ID;
            model.ItemID = businessEntity.ItemID;
            model.Quantity = businessEntity.Quantity;
            model.StoreID = businessEntity.StoreID;
            model.StatusID = businessEntity.StatusID;

            if (businessEntity.Definition != null)
            {
                model.Definition = ShoppingCartItemDefinitionModel.FromBusinessEntity(businessEntity.Definition);
            }

            if (businessEntity.State != null)
            {
                model.State = ShoppingCartItemStateModel.FromBusinessEntity(businessEntity.State);
            }

            if (businessEntity.Store != null)
            {
                model.Store = StoreModel.FromBusinessEntity(businessEntity.Store);
            }

            return model;
        }
    }
}