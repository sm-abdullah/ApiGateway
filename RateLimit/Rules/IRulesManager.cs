using RateLimit.Models;
namespace RateLimit
{
    public interface IRulesManager
    {
        RuleLimit GetMatchingRule(ClientRequest clientRequest);
    }
}
