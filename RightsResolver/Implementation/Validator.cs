using System.Collections.Generic;
using System.Linq;

namespace RightsResolver
{
    public class Validator
    {
        private Platform[] productAccessOnly = { Platform.Support };

        public bool IsValid(List<Rule> rules)
        {
            foreach (var rule in rules)
            {
                foreach (var platform in rule.ProductAccesses.Keys)
                {
                    if (!productAccessOnly.Contains(platform)) return false;
                }
                foreach (var platform in rule.PlatformAccesses.Keys)
                {
                    if (productAccessOnly.Contains(platform)) return false;
                }
            }

            return true;
        }
    }
}