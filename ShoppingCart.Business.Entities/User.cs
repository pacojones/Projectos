using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Business.Entities
{
    public class User : BusinessEntity
    {
        public long TypeID { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool Enabled { get; set; }

        public UserType Type { get; set; }
    }
}
