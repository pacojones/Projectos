using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace ShoppingCart.Presentation.Web.Models
{
    public class StoreModel : BaseDescriptionModel
    {
        public static StoreModel FromBusinessEntity(Business.Entities.Store businessEntity)
        {
            StoreModel model = new Models.StoreModel();
            model.Id = businessEntity.ID;
            model.Code = businessEntity.Code;
            model.Description = businessEntity.Description;

            return model;
        }

        public static Collection<StoreModel> FromBusinessEntityCollection(Collection<Business.Entities.Store> businessEntities)
        {
            if (businessEntities == null)
                return null;

            Collection<StoreModel> model = new Collection<Models.StoreModel>();

            foreach (var businessEntity in businessEntities)
            {
                model.Add(StoreModel.FromBusinessEntity(businessEntity));
            }

            return model;
        }
    }
}