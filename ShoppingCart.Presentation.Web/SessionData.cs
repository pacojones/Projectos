using ShoppingCart.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Presentation.Web
{
    public class SessionData
    {
        private const string CONTEXT = "Context";

        public static DataContext Context
        {
            get
            {
                return (DataContext)HttpContext.Current.Session[CONTEXT];
            }
            set
            {
                HttpContext.Current.Session[CONTEXT] = value;
            }
        }
    }
}