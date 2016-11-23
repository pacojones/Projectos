using ShoppingCart.Common;
using ShoppingCart.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Business.Entities;
using System.Collections.ObjectModel;
using ShoppingCart.DataAccess.SqlServer;

namespace ShoppingCart.Business.Core
{
    public class StoreManager : BaseManager, IStoreManager
    {
        private IDataMapper<Entities.Store> storeDataMapper;

        public StoreManager(DataContext context) : base(context)
        {
            this.storeDataMapper = new StoreDataMapper(context);
        }

        public Collection<Store> SelectStores()
        {
            return storeDataMapper.GetByCriteria(new EmptySearchCriteria());
        }
    }
}
