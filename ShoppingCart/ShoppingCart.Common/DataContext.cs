using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Common
{
    [Serializable]
    public class DataContext
    {
        public long ApplicationID { get; set; }

        public string Currency { get; set; }

        public string Language { get; set; }

        public string Username { get; set; }

        public DataContext()
        {

        }

        public DataContext(long applicationId, string currency, string language, string username)
        {
            this.ApplicationID = applicationId;
            this.Currency = currency;
            this.Language = language;
            this.Username = username;
        }
    }
}
