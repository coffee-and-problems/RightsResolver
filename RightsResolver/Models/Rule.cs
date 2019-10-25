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
        public List<ProductAccess> ProductAccesses { get; }
        public List<PlatformAccess> PlatformAccesses { get; }


        public Rule(int department, string post, List<ProductAccess> productAccesses,
                List<PlatformAccess> platformAccesses)
        {
            Department = department;
            Post = post;
            ProductAccesses = productAccesses;
            PlatformAccesses = platformAccesses;
        }
    }
}
