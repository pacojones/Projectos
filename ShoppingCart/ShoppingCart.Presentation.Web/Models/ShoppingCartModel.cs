using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Presentation.Web.Models
{
    public class ShoppingCartModel : BaseModel
    {
        private Collection<ShoppingCartItemModel> _items;

        public long OwnerID { get; set; }

        public long StatusID { get; set; }

        public ShoppingCartStateModel State { get; set; }

        public UserModel Owner { get; set; }

        public Collection<StoreModel> Stores { get; set; }

        public Collection<ShoppingCartItemModel> Items
        {
            get
            {
                if (_items == null)
                    _items = new Collection<ShoppingCartItemModel>();

                return _items;
            }
        }

        public static ShoppingCartModel FromBusinessEntity(Business.Entities.ShoppingCart businessEntity)
        {
            ShoppingCartModel model = new Models.ShoppingCartModel();

            model.Id = businessEntity.ID;
            model.OwnerID = businessEntity.OwnerID;
            model.StatusID = businessEntity.StatusID;

            if (businessEntity.State != null)
            {
                model.State = ShoppingCartStateModel.FromBusinessEntity(businessEntity.State);
            }

            if (businessEntity.Owner != null)
            {
                model.Owner = UserModel.FromBusinessEntity(businessEntity.Owner);
            }

            foreach(var businessItem in businessEntity.Items)
            {
                ShoppingCartItemModel item = ShoppingCartItemModel.FromBusinessEntity(businessItem);
                model.Items.Add(item);
            }

            return model;
        }
    }
}