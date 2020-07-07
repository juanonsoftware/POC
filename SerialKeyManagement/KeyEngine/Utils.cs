using System.Collections.Generic;

namespace KeyEngine
{
    internal sealed class Utils
    {
        public static IList<Item> ToList<TKey, TValue>(IDictionary<TKey, TValue> parameters)
        {
            var items = new List<Item>();
            foreach (var item in parameters)
            {
                items.Add(new Item(item.Key, item.Value));
            }
            return items;
        }

        public static IDictionary<TKey, TValue> ToDict<TKey, TValue>(IList<Item> items)
        {
            IDictionary<TKey, TValue> parameters = new Dictionary<TKey, TValue>();
            foreach (var item in items)
            {
                parameters.Add((TKey)item.Key, (TValue)item.Value);
            }
            return parameters;
        }
    }
}