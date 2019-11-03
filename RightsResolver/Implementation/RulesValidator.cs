using System.Collections.Generic;
using System.Linq;

namespace RightsResolver
{
    public class RulesValidator
    {
        private Platform[] platformAccessOnly =
        {
            Platform.Health,
            Platform.Manager,
            Platform.Oorv
        };

        public bool IsValid(List<Rule> rules)
        {
            foreach (var rule in rules)
            {
                foreach (var platform in rule.ProductAccesses.Keys)
                {
                    if (platformAccessOnly.Contains(platform)) return false;
                }
                foreach (var platform in rule.PlatformAccesses.Keys)
                {
                    if (!platformAccessOnly.Contains(platform)) return false;
                }
            }

            return true;
        }
    }
}