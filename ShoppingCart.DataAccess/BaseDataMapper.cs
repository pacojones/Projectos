using ShoppingCart.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ShoppingCart.DataAccess
{
    /// <summary>
    /// Class BaseDataMapper.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ShoppingCart.DataAccess.IDataMapper{T}" />
    public class BaseDataMapper
    {
        public DataContext Context { get; set; }
    }
}
