using System.Collections.Generic;

namespace RightsResolver
{
    public class Rights
    {
        public Dictionary<Platform, Role> PlatformAccesses {get;}
        public Dictionary<Platform, List<ProductRole>> ProductAccesses { get; }

        public Rights(Dictionary<Platform, Role> platformAccesses,
            Dictionary<Platform, List<ProductRole>> productAccesses)
        {
            PlatformAccesses = platformAccesses;
            ProductAccesses = productAccesses;
        }
    }
}