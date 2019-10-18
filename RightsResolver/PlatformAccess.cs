using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    class PlatformAccess : IAccess
    {
        public string Platform { get; }
        public Role Role { get; }
    }
}
