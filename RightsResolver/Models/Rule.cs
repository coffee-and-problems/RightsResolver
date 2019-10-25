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
        public List<ProductAccessDTO> ProductAccesses { get; }
        public List<PlatformAccessDTO> PlatformAccesses { get; }

        public Rule(int department, string post, List<ProductAccessDTO> productAccesses,
                List<PlatformAccessDTO> platformAccesses)
        {
            Department = department;
            Post = post;
            ProductAccesses = productAccesses;
            PlatformAccesses = platformAccesses;
        }
    }
}
