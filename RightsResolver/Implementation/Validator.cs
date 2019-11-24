using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using RightsResolver.Models;

namespace RightsResolver.Implementation
{
    public class Validator
    {
        private readonly Platform[] productAccessOnly = { Platform.Support };

        public bool IsValid([NotNull] Rule rule)
        {
            foreach (var platform in rule.ProductAccesses.Keys)
            {
                if (!productAccessOnly.Contains(platform)) return false;
            }
            foreach (var platform in rule.PlatformAccesses.Keys)
            {
                if (productAccessOnly.Contains(platform)) return false;
            }

            return true;
        }
    }
}