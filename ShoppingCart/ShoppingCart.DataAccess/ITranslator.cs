using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess
{
    public interface ITranslator<B, D>
    {
        /// <summary>
        /// Gets or sets the data entity.
        /// </summary>
        /// <value>The data entity.</value>
        D DataEntity { get; set; }

        /// <summary>
        /// To the business entity.
        /// </summary>
        /// <returns>B.</returns>
        B ToBusinessEntity();
    }
}
