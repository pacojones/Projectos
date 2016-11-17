using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Business.Core
{
    public interface IStoreManager
    {
        Collection<Entities.Store> SelectStores();
    }
}
