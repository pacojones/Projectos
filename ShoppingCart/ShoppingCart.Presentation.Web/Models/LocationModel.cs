using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Presentation.Web.Models
{
    public class LocationModel : BaseDescriptionModel
    {
        public long Order { get; set; }

        public static LocationModel FromBusinessEntity(Business.Entities.Location businessEntity)
        {
            LocationModel model = new Models.LocationModel();
            model.Id = businessEntity.ID;
            model.Code = businessEntity.Code;
            model.Description = businessEntity.Description;
            model.Order = businessEntity.Order;

            return model;
        }
    }
}