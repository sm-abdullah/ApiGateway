
using RateLimit.Models;

namespace RateLimit
{
    public interface IRateLimitSettingManager
    {
        RateLimitPolicies RateLimitPolicies { get; }
        RateLimitSettings RateLimitSettings { get; }
        IClientResolver ClientResolver { get; }
        ICounterKeyBuilder CounterKeyBuilder { get; }
    }
}
