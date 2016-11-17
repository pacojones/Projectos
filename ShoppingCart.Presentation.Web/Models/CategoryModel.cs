using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Presentation.Web.Models
{
    public class CategoryModel : BaseDescriptionModel
    {
        public static CategoryModel FromBusinessEntity(Business.Entities.Category businessEntity)
        {
            CategoryModel model = new Models.CategoryModel();
            model.Id = businessEntity.ID;
            model.Code = businessEntity.Code;
            model.Description = businessEntity.Description;

            return model;
        }
    }
}