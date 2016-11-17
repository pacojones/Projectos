using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Presentation.Web.Models
{
    public class ShoppingCartItemStateModel : BaseDescriptionModel
    {
        public static ShoppingCartItemStateModel FromBusinessEntity(Business.Entities.ShoppingCartItemState businessEntity)
        {
            ShoppingCartItemStateModel model = new Models.ShoppingCartItemStateModel();
            model.Id = businessEntity.ID;
            model.Code = businessEntity.Code;
            model.Description = businessEntity.Description;

            return model;
        }
    }
}