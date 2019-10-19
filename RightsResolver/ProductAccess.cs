using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    class ProductAccess : IAccess
    {
        public string Platform { get; }
        public List<ProductRole> Products { get; }
    }

    class ProductRole
    {
        public string ProductId { get; }
        public Role role { get; }
    }
}
