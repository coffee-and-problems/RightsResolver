using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using RightsResolver.BusinessObjects;
using RightsResolver.Implementation.Extensions;
using RightsResolver.Models;

namespace RightsResolver.Implementation
{
    public class Merger
    {
        [NotNull]
        public Rights MergeRights([NotNull] List<Rights> allRights)
        {
            var platformAccesses = allRights.Select(right => right.PlatformAccesses).ToList();
            var productAccesses = allRights.Select(right => right.ProductAccesses).ToList();

            var mergedPlatformAccesses = DictionaryExtension.MergeDictionaries(platformAccesses, EnumExtension.Max);
            var mergedProductAccesses = DictionaryExtension.MergeDictionaries(productAccesses,
                (roles1, roles2) => DictionaryExtension.MergeDictionaries(
                    new List<Dictionary<string, Role>>() {roles1, roles2}, EnumExtension.Max));

            return new Rights(mergedPlatformAccesses, mergedProductAccesses);
        }
    }
}