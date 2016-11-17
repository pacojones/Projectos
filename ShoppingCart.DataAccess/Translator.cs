using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess
{
    public class Translator<B, D> : ITranslator<B, D>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Translator{B, D}"/> class.
        /// </summary>
        /// <param name="dataEntity">The data entity.</param>
        public Translator(D dataEntity)
        {
            this.DataEntity = dataEntity;
        }

        /// <summary>
        /// Gets or sets the data entity.
        /// </summary>
        /// <value>The data entity.</value>
        public D DataEntity { get; set; }

        /// <summary>
        /// To the business entity.
        /// </summary>
        /// <returns>B.</returns>
        public virtual B ToBusinessEntity()
        {
            return default(B);
        }
    }
}
