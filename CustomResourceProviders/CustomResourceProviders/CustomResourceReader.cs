using System;
using System.Collections;
using System.Resources;

namespace CustomResourceProviders
{
    internal sealed class CustomResourceReader : IResourceReader
    {
        private IDictionary _resources;

        public CustomResourceReader(IDictionary resources)
        {
            _resources = resources;
        }

        IDictionaryEnumerator IResourceReader.GetEnumerator()
        {
            return _resources.GetEnumerator();
        }

        void IResourceReader.Close() { }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _resources.GetEnumerator();
        }

        void IDisposable.Dispose() { return; }
    }
}