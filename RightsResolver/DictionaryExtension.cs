using System;
using System.Collections.Generic;

namespace RightsResolver
{
    public static class DictionaryExtension
    {
        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary,
            TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else dictionary.Add(key, value);
        }
    }
}