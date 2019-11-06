using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    public class UserRights
    {
        public Guid UserId { get; }
        public Dictionary<Platform, Role> PlatformAccesses {get;}
        public Dictionary<Platform, List<ProductRole>> ProductAccess { get; }

        public UserRights(Guid userId, Dictionary<Platform, Role> platformAccesses,
            Dictionary<Platform, List<ProductRole>> productAccess)
        {
            UserId = userId;
            PlatformAccesses = platformAccesses;
            ProductAccess = productAccess;
        }
    }
}
