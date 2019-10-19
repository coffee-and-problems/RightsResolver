using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    class Rule
    {
        private Position position;
        public List<IAccess> Access { get; }
    }
}
