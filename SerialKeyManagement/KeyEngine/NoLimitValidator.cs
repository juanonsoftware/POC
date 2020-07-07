namespace KeyEngine
{
    /// <summary>
    /// Any key is considered as valid
    /// </summary>
    public class NoLimitValidator : IKeyValidator
    {
        public bool Validate(string key)
        {
            return true;
        }
    }
}
