using System.Collections.Generic;

namespace RateLimit.Models
{
    /// <summary>
    /// Rigt now we will reading these rules from configuration which is technically not much scaleable
    /// but later it can be pulled from database though you need to write provider for that
    /// </summary>
    public class RateLimitPolicies
    {
        public List<ClientProfile> ClientRules { get; set; }
    }

    public class ClientProfile
    {
        public string ClientId { get; set; }
        public List<ClientRules> Rules { get; set; }
    }
    public class ClientRules
    {
        public string Endpoint { get; set; }
        public string Period { get; set; }
        public int Limit { get; set; }
    }

}
