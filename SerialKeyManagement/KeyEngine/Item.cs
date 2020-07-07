using System;

namespace KeyEngine
{
    [Serializable]
    public class Item
    {
        public object Key { get; set; }

        public object Value { get; set; }

        public Item()
        {
        }

        public Item(object key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}