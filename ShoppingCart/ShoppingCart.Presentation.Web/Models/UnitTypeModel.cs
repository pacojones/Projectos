using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Presentation.Web.Models
{
    public class UnitTypeModel : BaseDescriptionModel
    {
        public static UnitTypeModel FromBusinessEntity(Business.Entities.UnitType businessEntity)
        {
            UnitTypeModel model = new Models.UnitTypeModel();
            model.Id = businessEntity.ID;
            model.Code = businessEntity.Code;
            model.Description = businessEntity.Description;

            return model;
        }
    }
}