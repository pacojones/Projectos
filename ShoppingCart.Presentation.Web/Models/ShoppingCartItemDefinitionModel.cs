using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Presentation.Web.Models
{
    public class ShoppingCartItemDefinitionModel : BaseDescriptionModel
    {
        public long CategoryID { get; set; }

        public long UnitID { get; set; }

        public long LocationID { get; set; }

        public decimal UnitPrice { get; set; }

        public long DefaultStoreID { get; set; }

        public decimal DefaultQuantity { get; set; }

        public bool Enabled { get; set; }

        public CategoryModel Category { get; set; }

        public UnitTypeModel Unit { get; set; }

        public LocationModel Location { get; set; }

        public StoreModel DefaultStore { get; set; }

        public static ShoppingCartItemDefinitionModel FromBusinessEntity(Business.Entities.ShoppingCartItemDefinition businessEntity)
        {
            ShoppingCartItemDefinitionModel model = new Models.ShoppingCartItemDefinitionModel();

            model.CategoryID = businessEntity.CategoryID;
            model.DefaultQuantity = businessEntity.DefaultQuantity;
            model.DefaultStoreID = businessEntity.DefaultStoreID;
            model.Enabled = businessEntity.Enabled;
            model.LocationID = businessEntity.LocationID;
            model.UnitPrice = businessEntity.UnitPrice;
            model.UnitID = businessEntity.UnitID;
            model.Code = businessEntity.Code;
            model.Description = businessEntity.Description;

            if (businessEntity.Category != null)
            {
                model.Category = CategoryModel.FromBusinessEntity(businessEntity.Category);
            }

            if (businessEntity.Location != null)
            {
                model.Location = LocationModel.FromBusinessEntity(businessEntity.Location);
            }

            if (businessEntity.DefaultStore != null)
            {
                model.DefaultStore = StoreModel.FromBusinessEntity(businessEntity.DefaultStore);
            }

            if (businessEntity.Unit != null)
            {
                model.Unit = UnitTypeModel.FromBusinessEntity(businessEntity.Unit);
            }

            return model;
        }
    }
}