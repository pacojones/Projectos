using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Presentation.Web.Models
{
    public class UserTypeModel : BaseDescriptionModel
    {
        public static UserTypeModel FromBusinessEntity(Business.Entities.UserType businessEntity)
        {
            UserTypeModel model = new Models.UserTypeModel();
            model.Id = businessEntity.ID;
            model.Code = businessEntity.Code;
            model.Description = businessEntity.Description;

            return model;
        }
    }
}