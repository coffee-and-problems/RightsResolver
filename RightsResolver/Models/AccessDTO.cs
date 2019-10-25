using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    public class PlatformAccessDTO
    {
        public string Platform { get; }
        public Role Role { get; }

        public PlatformAccessDTO(string platform, Role role)
        {
            Platform = platform;
            Role = role;
        }
    }

    public class ProductAccessDTO
    {
        public string Platform { get; }
        public List<ProductRole> Products { get; }

        public ProductAccessDTO(string platform, List<ProductRole> products)
        {
            Platform = platform;
            Products = products;
        }
    }
}
