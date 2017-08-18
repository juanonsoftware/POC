using Resources.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Resources.Abstract
{
    public abstract class BaseResourceProvider : IResourceProvider
    {
        private readonly string _providerKey;

        // Cache list of resources
        private readonly static IDictionary<string, IDictionary<string, ResourceEntry>> resources = new Dictionary<string, IDictionary<string, ResourceEntry>>();
        private static readonly object lockResources = new object();

        protected BaseResourceProvider(string providerKey)
        {
            _providerKey = providerKey;
            Cache = true; // By default, enable caching for performance
        }

        protected bool Cache { get; set; } // Cache resources ?

        /// <summary>
        /// Returns a single resource for a specific culture
        /// </summary>
        /// <param name="name">Resorce name (ie key)</param>
        /// <param name="culture">Culture code</param>
        /// <returns>Resource</returns>
        public object GetResource(string name, string culture)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Resource name cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(culture))
                throw new ArgumentException("Culture name cannot be null or empty.");

            // normalize
            culture = culture.ToLowerInvariant();

            if (Cache)
            {
                // Fetch all resources
                if (!resources.ContainsKey(_providerKey))
                {
                    lock (lockResources)
                    {
                        if (!resources.ContainsKey(_providerKey))
                        {
                            var res = ReadResources().ToDictionary(r => string.Format("{0}.{1}", r.Culture.ToLowerInvariant(), r.Name));
                            resources.Add(_providerKey, res);
                        }
                    }
                }

                return resources[_providerKey][string.Format("{0}.{1}", culture, name)].Value;
            }

            return ReadResource(name, culture).Value;
        }


        /// <summary>
        /// Returns all resources for all cultures. (Needed for caching)
        /// </summary>
        /// <returns>A list of resources</returns>
        protected abstract IList<ResourceEntry> ReadResources();


        /// <summary>
        /// Returns a single resource for a specific culture
        /// </summary>
        /// <param name="name">Resorce name (ie key)</param>
        /// <param name="culture">Culture code</param>
        /// <returns>Resource</returns>
        protected abstract ResourceEntry ReadResource(string name, string culture);

    }
}
