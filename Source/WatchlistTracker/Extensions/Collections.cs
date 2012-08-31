using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WatchlistTracker.Extensions
{
    public static class Collections
    {
        public static TValue GetValueOrDefault<TKey,TValue>(this IDictionary<TKey,TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            if (dict.ContainsKey(key))
                return dict[key];
            return defaultValue;
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach( var item in collection)
            {
                action(item);
            }
        }
    }
}