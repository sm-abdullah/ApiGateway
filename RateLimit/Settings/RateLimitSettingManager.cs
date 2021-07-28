
using Microsoft.Extensions.Options;
using RateLimit;
using RateLimit.Models;

namespace RateLimit
{
    public class RateLimitSettingManager : IRateLimitSettingManager
    {
        private IClientResolver _clientResolver;
        private IOptions<RateLimitPolicies> _rateLimitPolicies;
        private IOptions<RateLimitSettings> _rateLimitSettings;
        private ICounterKeyBuilder _clientKeyBuilder;
        public RateLimitSettingManager(IOptions<RateLimitPolicies> rateLimitPolicies, IOptions<RateLimitSettings> rateLimitSettings) 
        {
            _clientResolver = new ClientIDResolver(rateLimitSettings?.Value.ClientIdHeader);
            _rateLimitPolicies = rateLimitPolicies;
            _rateLimitSettings = rateLimitSettings;
            _clientKeyBuilder = new ClientCounterKeyBuilder();
        }
        public RateLimitPolicies RateLimitPolicies =>  _rateLimitPolicies?.Value;

        public RateLimitSettings RateLimitSettings => _rateLimitSettings?.Value;

        public IClientResolver ClientResolver => _clientResolver;

        public ICounterKeyBuilder CounterKeyBuilder => _clientKeyBuilder;
    }
}
