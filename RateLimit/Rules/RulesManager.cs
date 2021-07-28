using RateLimit.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RateLimit
{

    public class RulesManager : IRulesManager
    {
        private IRateLimitSettingManager _rateLimitSettingManager;
        private Dictionary<string, ClientProfile> _clientProfiles;
        public RulesManager(IRateLimitSettingManager rateLimitSettingManager)
        {
            _rateLimitSettingManager = rateLimitSettingManager;
            _clientProfiles = _rateLimitSettingManager.RateLimitPolicies.ClientRules.ToDictionary(item => item.ClientId);
        }
        public RuleLimit GetMatchingRule(ClientRequest clientRequest)
        {
            ClientProfile profile = null;
            if (_clientProfiles.ContainsKey(clientRequest.ClientId))
            {
                profile = _clientProfiles[clientRequest.ClientId];
            }
            else if (_clientProfiles.ContainsKey("*")) // pic default 
            {
                profile = _clientProfiles["*"];
            }

            if (profile != null)
            {
                return new RuleLimit()
                {
                    // we are using CounterKeyBuilder it will gives fleciblity to scale it later
                    // basically idea is here you should be able to generate uniqe key
                    // for each individual rule that is aplicable for every single request which is out of the scope
                    // so that why i am only focusing on client id
                    Key = _rateLimitSettingManager.CounterKeyBuilder.Build(clientRequest),
                    // as of now we are supporting one rule per client so picking first one
                    // but it is scaleable , so you can specifiy rules by client then by endpoint and verb
                    // so based on client request you will pick best matching rule
                    Count = profile.Rules.First().Limit,
                    TimeSpan = profile.Rules.First().Period.ToTimeSpan(), //todo syed
                };
            }
            
            return null;
        }
    }
}
