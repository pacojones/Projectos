using ShoppingCart.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.SqlServer
{
    public class ShoppingCartItemDataMapper : SqlDataMapper, IDataMapper<Business.Entities.ShoppingCartItem>
    {
        public ShoppingCartItemDataMapper(DataContext context) : base(context) { }

        public long Add(Business.Entities.ShoppingCartItem item)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Collection<Business.Entities.ShoppingCartItem> GetByCriteria(SearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public Business.Entities.ShoppingCartItem GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Business.Entities.ShoppingCartItem GetByKey(object key)
        {
            throw new NotImplementedException();
        }

        public void Update(Business.Entities.ShoppingCartItem item)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add(ParametersNames.ShoppingCartItemID, item.ID);
            parameters.Add(ParametersNames.Quantity, item.Quantity);
            parameters.Add(ParametersNames.StoreID, item.StoreID);

            this.sqlHelper.ExecuteNonQuery(ProceduresNames.ShoppingCartItemUpdate, parameters);
        }

        public void Update(Business.Entities.ShoppingCartItem item, BaseCriteria criteria)
        {
            throw new NotImplementedException();
        }

        #region Subclasses

        /// <summary>
        /// Class ProceduresNames.
        /// </summary>
        public class ProceduresNames
        {
            /// <summary>
            /// 
            /// </summary>
            public const string ShoppingCartItemUpdate = "SP_ShoppingCartItemUpdate";
        }

        public class ParametersNames
        {
            public const string ShoppingCartItemID = "@I_ShoppingCartItemID";

            public const string Quantity = "@I_Quantity";

            public const string StoreID = "@I_StoreID";
        }

            #endregion
    }
}
