using System.Collections.Generic;

namespace XmlTypeForMultipleProperties
{
    public static class DictionaryHelper
    {
        public static void AddOrSet(this IDictionary<string, string> dict, string key, string value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }
        }

        public static string GetOrDefault(this IDictionary<string, string> dict, string key)
        {
            return dict.ContainsKey(key) ? dict[key] : default(string);
        }
    }
}