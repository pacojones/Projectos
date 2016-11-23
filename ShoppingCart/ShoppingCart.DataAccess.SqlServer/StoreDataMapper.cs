using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Business.Entities;
using ShoppingCart.Common;

namespace ShoppingCart.DataAccess.SqlServer
{
    public class StoreDataMapper : SqlDataMapper, IDataMapper<Business.Entities.Store>
    {
        public StoreDataMapper(DataContext context) : base(context) { }

        public long Add(Store item)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Collection<Store> GetByCriteria(SearchCriteria criteria)
        {
            var items = this.sqlHelper.ExecuteReader(ProceduresNames.StoreListSelect, null);

            StoreListTranslator translator = new SqlServer.StoreListTranslator(items);

            return translator.ToBusinessEntity();
        }

        public Store GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Store GetByKey(object key)
        {
            throw new NotImplementedException();
        }

        public void Update(Store item)
        {
            throw new NotImplementedException();
        }

        public void Update(Store item, BaseCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public class ProceduresNames
        {
            /// <summary>
            /// 
            /// </summary>
            public const string StoreListSelect = "SP_StoreListSelect";
        }
    }
}
