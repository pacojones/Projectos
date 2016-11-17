using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Presentation.Web.Models
{
    public class ShoppingCartStateModel : BaseDescriptionModel
    {
        public static ShoppingCartStateModel FromBusinessEntity(Business.Entities.BusinessDescriptionEntity businessEntity)
        {
            return new Models.ShoppingCartStateModel()
            {
                Id = businessEntity.ID,
                Code = businessEntity.Code,
                Description = businessEntity.Description
            };
        }
    }
}