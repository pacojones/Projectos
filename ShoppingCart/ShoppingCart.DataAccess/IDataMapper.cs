using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess
{
    public interface IDataMapper<T>
    {
        long Add(T item);

        T GetById(long id);

        T GetByKey(object key);

        Collection<T> GetByCriteria(SearchCriteria criteria);

        void Update(T item);

        void Update(T item, BaseCriteria criteria);

        void Delete(long id);
    }
}
