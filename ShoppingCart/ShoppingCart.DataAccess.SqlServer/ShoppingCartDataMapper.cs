using ShoppingCart.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Business.Entities;
using System.Collections.ObjectModel;
using ShoppingCart.Common;

namespace ShoppingCart.DataAccess.SqlServer
{
    public class ShoppingCartDataMapper : SqlDataMapper, IDataMapper<Business.Entities.ShoppingCart>
    {
        public ShoppingCartDataMapper(DataContext context) : base(context) { }

        public long Add(Business.Entities.ShoppingCart item)
        {
            this.sqlHelper.ExecuteNonQuery(ProceduresNames.ShoppingCartInsert, null);

            if (!this.sqlHelper.OutputResult.HasValue)
                throw new InvalidOperationException();

            return this.sqlHelper.OutputResult.Value;
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Collection<Business.Entities.ShoppingCart> GetByCriteria(SearchCriteria criteria)
        {
            if (criteria is ShoppingCartListSearchCriteria)
            {
                return this.GetShoppingCartList(criteria);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private Collection<Business.Entities.ShoppingCart> GetShoppingCartList(SearchCriteria criteria)
        {
            var items = this.sqlHelper.ExecuteReader(ProceduresNames.ShoppingCartListSelect, null);

            ShoppingCartListTranslator translator = new SqlServer.ShoppingCartListTranslator(items);

            return translator.ToBusinessEntity();
        }

        public Business.Entities.ShoppingCart GetById(long id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add(ParametersNames.ShoppingCartID, id);

            var items = this.sqlHelper.ExecuteReader(ProceduresNames.ShoppingCartSelect, parameters);

            ShoppingCartTranslator translator = new SqlServer.ShoppingCartTranslator(items);

            return translator.ToBusinessEntity();
        }

        public Business.Entities.ShoppingCart GetByKey(object key)
        {
            throw new NotImplementedException();
        }

        public void Update(Business.Entities.ShoppingCart item)
        {
            throw new NotImplementedException();
        }

        public void Update(Business.Entities.ShoppingCart item, BaseCriteria criteria)
        {
            Validators.IsNotNull(item, "item");

            if (!(criteria is ShoppingCartUpdateCriteria))
            {
                throw new InvalidOperationException("criteria must be of type ShoppingCartUpdateCriteria");
            }

            ShoppingCartUpdateCriteria shoppingCartUpdateCriteria = (ShoppingCartUpdateCriteria)criteria;

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add(ParametersNames.ShoppingCartID, item.ID);
            parameters.Add(ParametersNames.ActionID, shoppingCartUpdateCriteria.ActionID);
            parameters.Add(ParametersNames.ShoppingCartItems, SerializationHelper.SerializeObject(item.Items));

            this.sqlHelper.ExecuteNonQuery(ProceduresNames.ShoppingCartUpdate, parameters);
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
            public const string ShoppingCartInsert = "SP_ShoppingCartInsert";

            public const string ShoppingCartSelect = "SP_ShoppingCartSelect";

            public const string ShoppingCartListSelect = "SP_ShoppingCartListSelect";

            public const string ShoppingCartUpdate = "SP_ShoppingCartUpdate";
        }

        public class ParametersNames
        {
            public const string ShoppingCartID = "@I_ShoppingCartID";

            public const string ActionID = "@I_ActionID";

            public const string ShoppingCartItems = "@I_ShoppingCartItems";
        }

            #endregion
        }
}
