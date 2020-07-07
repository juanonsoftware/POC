using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace KeyEngine
{
    public class KeyBuilder
    {
        private readonly IDictionary<string, string> _keyParameters = new Dictionary<string, string>();

        public KeyBuilder Initialize(string identity, DateTime expireDate)
        {
            _keyParameters.Add(Constants.Identity, identity);
            _keyParameters.Add(Constants.ExpireDate, expireDate.ToString(CultureInfo.InvariantCulture));

            _keyParameters.Add("CreationDate", DateTime.Now.ToString(CultureInfo.InvariantCulture));
            _keyParameters.Add("CreationHostName", Dns.GetHostName());

            return this;
        }

        /// <summary>
        /// Add or update optional parameters
        /// </summary>
        public void AddOrUdate(string parameterName, object parameterValue)
        {
            if (_keyParameters.ContainsKey(parameterName))
            {
                _keyParameters[parameterName] = parameterValue.ToString();
            }
            else
            {
                _keyParameters.Add(parameterName, parameterValue.ToString());
            }
        }

        /// <summary>
        /// Create key string from parameters added previously
        /// </summary>
        public string Build(IParameterEncoder parameterEncoder)
        {
            return parameterEncoder.Encode(_keyParameters);
        }
    }
}