using System;
using System.Collections.Generic;
using System.Linq;

namespace RightsResolver
{
    public class Merger
    {
        public Rights MergeRights(List<Rights> allRights)
        {
            var platformAccesses = allRights.Select(right => right.PlatformAccesses).ToList();
            var productAccesses = allRights.Select(right => right.ProductAccesses).ToList();

            return new Rights(MergeDictionary(platformAccesses, EnumExtention.Max),
                MergeDictionary(productAccesses, 
                    (roles1, roles2) => MergeDictionary(
                        new List<Dictionary<string, Role>>() {roles1, roles2}, EnumExtention.Max)));
        }

        private Dictionary<TKey, TValue> MergeDictionary<TKey, TValue>(
            List<Dictionary<TKey, TValue>> list, Func<TValue, TValue, TValue> mergeRule)
        {
            var merged = new Dictionary<TKey, TValue>();

            foreach (var dictionary in list)
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