using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.SqlServer
{
    public class StoreListTranslator : Translator<Collection<Business.Entities.Store>, DataSet>
    {
        public StoreListTranslator(DataSet ds) : base(ds) { }

        public override Collection<Business.Entities.Store> ToBusinessEntity()
        {
            DataTable storesTable = this.DataEntity.Tables[0];
            Collection<Business.Entities.Store> stores = null;

            if (this.DataEntity.Tables.Count > 0)
            {
                stores = new Collection<Business.Entities.Store>();

                foreach (DataRow row in storesTable.Rows)
                {
                    Business.Entities.Store store = new Business.Entities.Store();
                    store.ID = row.Field<long>("ID");
                    store.Code = row.Field<string>("Code");
                    store.Description = row.Field<string>("Description");

                    stores.Add(store);
                }
            }

            return stores;
        }
    }
}
