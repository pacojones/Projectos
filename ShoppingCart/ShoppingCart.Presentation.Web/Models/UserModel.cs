using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Presentation.Web.Models
{
    public class UserModel : BaseModel
    {
        public long TypeID { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool Enabled { get; set; }

        public UserTypeModel Type { get; set; }

        public static UserModel FromBusinessEntity(Business.Entities.User businessEntity)
        {
            UserModel model = new UserModel();

            model.Id = businessEntity.ID;
            model.TypeID = businessEntity.TypeID;
            model.Username = businessEntity.Username;
            model.Name = businessEntity.Name;
            model.Email = businessEntity.Email;
            model.Enabled = businessEntity.Enabled;

            if (businessEntity.Type != null)
            {
                model.Type = UserTypeModel.FromBusinessEntity(businessEntity.Type);
            }

            return model;
        }
    }
}