using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    public class ProductAccess : IAccess
    {
        public string Platform { get; }
        public List<ProductRole> Products { get; }

        public ProductAccess(string platform, List<ProductRole> products)
        {
            Platform = platform;
            Products = products;
        }
    }

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
