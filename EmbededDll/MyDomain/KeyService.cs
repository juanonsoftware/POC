using System;

namespace MyDomain
{
    public class KeyService
    {
        public string GetNewKey()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }
    }
}
