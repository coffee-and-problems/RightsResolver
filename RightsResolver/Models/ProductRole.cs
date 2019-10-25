using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    public class ProductRole
    {
        public string ProductId { get; }
        public Role Role { get; }

        public ProductRole(string productId, Role role)
        {
            ProductId = productId;
            Role = role;
        }
    }
}
