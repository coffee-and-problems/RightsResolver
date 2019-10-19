using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    class Rule
    {
        public int Department { get; }
        public string Post { get; }
        public List<IAccess> Access { get; }
    }
}
