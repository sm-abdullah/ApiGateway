

namespace RateLimit
{
    public interface ICounterKeyBuilder
    {
        string Build(ClientRequest requestIdentity);
    }
}
