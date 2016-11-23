using ShoppingCart.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.SqlServer
{
    public class SqlDataMapper : BaseDataMapper
    {
        protected DataContext context;

        protected SqlHelper sqlHelper;

        public DataContext Context
        {
            get
            {
                return context;
            }
        }

        public SqlDataMapper(DataContext context)
        {
            this.context = context;
            this.sqlHelper = new SqlHelper(context);
        }
    }
}
