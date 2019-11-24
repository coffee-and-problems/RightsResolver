using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace RightsResolver.Implementation.Extensions
{
    public static class DictionaryExtension
    {
        public static void AddOrUpdate<TKey, TValue>([NotNull] this Dictionary<TKey, TValue> dictionary,
            TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else dictionary.Add(key, value);
        }

        [NotNull]
        public static Dictionary<TKey, TValue> MergeDictionaries<TKey, TValue>(
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