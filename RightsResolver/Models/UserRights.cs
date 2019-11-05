using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    class UserRights
    {
        public int UserId { get; }
        public Dictionary<Platform, Role> PlatformAccesses {get;}
        public Dictionary<Platform, List<ProductRole>> ProductAccess { get; }
    }
}
