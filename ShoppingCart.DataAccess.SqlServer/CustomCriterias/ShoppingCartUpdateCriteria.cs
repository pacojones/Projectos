﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.SqlServer
{
    public class ShoppingCartUpdateCriteria : BaseCriteria
    {
        public long ActionID { get; set; }
    }
}
