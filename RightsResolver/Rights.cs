using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    class Right
    {

    }

    class Oorv
    {
        public bool haveAccess { get; }
        public Role role { get; }
    }

    class Health
    {
        public bool haveAccess { get; }
        public Role role { get; }
    }

    class Manager
    {
        public bool haveAccess { get; }
        public Role role { get; }
    }

    class Support
    {
        public bool haveAccess { get; }
        public Product[] products { get; }
    }
}
