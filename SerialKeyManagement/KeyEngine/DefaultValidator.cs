using System;
using System.Globalization;

namespace KeyEngine
{
    /// <summary>
    /// Validate if the key was created for a given identity and expire date is later then current time of the server.
    /// </summary>
    public class DefaultValidator : IKeyValidator
    {
        private readonly IParameterEncoder _parameterEncoder;
        private readonly string _identity;

        public DefaultValidator(IParameterEncoder parameterEncoder, string identity)
        {
            _parameterEncoder = parameterEncoder;
            _identity = identity;
        }

        public virtual bool Validate(string key)
        {
            var parameters = _parameterEncoder.Decode(key);

            return parameters.ContainsKey(Constants.Identity) &&
                   parameters.ContainsKey(Constants.ExpireDate) &&
                   parameters[Constants.Identity].Equals(_identity, StringComparison.InvariantCultureIgnoreCase) &&
                   DateTime.Parse(parameters[Constants.ExpireDate], CultureInfo.InvariantCulture) >= DateTime.Now;
        }
    }
}