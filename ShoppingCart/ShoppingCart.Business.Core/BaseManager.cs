using ShoppingCart.Common;
using ShoppingCart.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Business.Core
{
    public class BaseManager
    {
        public BaseManager (DataContext context)
        {
            this.Context = context;
        }

        public DataContext Context { get; set; }
    }
}
