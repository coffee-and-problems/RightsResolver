using System.Collections.Generic;

namespace RightsResolver
{
    public class Rights
    {
        public Dictionary<Platform, Role> PlatformAccesses {get;}
        public Dictionary<Platform, Dictionary<string, Role>> ProductAccesses { get; }

        public Rights(Dictionary<Platform, Role> platformAccesses,
            Dictionary<Platform, Dictionary<string, Role>> productAccesses)
        {
            PlatformAccesses = platformAccesses;
            ProductAccesses = productAccesses;
        }
    }
}