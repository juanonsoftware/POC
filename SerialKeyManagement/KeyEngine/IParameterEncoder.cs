using System.Collections.Generic;

namespace KeyEngine
{
    public interface IParameterEncoder
    {
        IDictionary<string, string> Decode(string text);

        string Encode(IDictionary<string, string> parameters);
    }
}