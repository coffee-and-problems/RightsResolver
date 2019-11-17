using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace RightsResolver
{
    public class Merger
    {
        [NotNull]
        public Rights MergeRights([NotNull] List<Rights> allRights)
        {
            var platformAccesses = allRights.Select(right => right.PlatformAccesses).ToList();
            var productAccesses = allRights.Select(right => right.ProductAccesses).ToList();

            return new Rights(MergeDictionaries(platformAccesses, EnumExtention.Max),
                MergeDictionaries(productAccesses, 
                    (roles1, roles2) => MergeDictionaries(
                        new List<Dictionary<string, Role>>() {roles1, roles2}, EnumExtention.Max)));
        }

        [NotNull]
        private Dictionary<TKey, TValue> MergeDictionaries<TKey, TValue>(
            [NotNull] List<Dictionary<TKey, TValue>> dictionaryList, Func<TValue, TValue, TValue> mergeRule)
        {
            var merged = new Dictionary<TKey, TValue>();

            foreach (var dictionary in dictionaryList)
            {
                foreach (var keyValue in dictionary)
                {
                    if (!merged.ContainsKey(keyValue.Key))
                        merged.Add(keyValue.Key, keyValue.Value);
                    else merged[keyValue.Key] = mergeRule(merged[keyValue.Key], keyValue.Value);
                }
            }

            return merged;
        }
    }
}