using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    public class Rule
    {
        public int Department { get; }
        public string Post { get; }
        public Dictionary<Platform, Role> PlatformAccesses { get; }
        public Dictionary<Platform, List<ProductRole>> ProductAccesses { get; }

        public Rule(int department, string post, Dictionary<Platform, List<ProductRole>> productAccesses,
            Dictionary<Platform, Role> platformAccesses)
        {
            Department = department;
            Post = post;
            ProductAccesses = productAccesses;
            PlatformAccesses = platformAccesses;
        }
    }
}
