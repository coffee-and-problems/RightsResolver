using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace RightsResolver
{
    public class Resolver
    {
        private string[] products;
        private string rulesPath;

        public Resolver(string rulesPath, string[] products)
        {
            this.rulesPath = rulesPath;
            this.products = products;
        }
    }
}
