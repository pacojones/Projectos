using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Common
{
    public static class Extensions
    {
        public static bool In<T>(this T source, params T[] list)
        {
            if (null == source)
                throw new ArgumentNullException("source");

            return list.Contains(source);
        }

        public static bool NotIn<T>(this T source, params T[] list)
        {
            if (null == source)
                throw new ArgumentNullException("source");

            return !list.Contains(source);
        }
    }
}
