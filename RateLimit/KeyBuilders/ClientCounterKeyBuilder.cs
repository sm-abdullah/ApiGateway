
namespace RateLimit
{
    /// <summary> 
    /// Client Counter key builder that willl be used as key in Cach
    /// can different implementation based on rules
    /// </summary>
    public class ClientCounterKeyBuilder : ICounterKeyBuilder
    {
        /// <summary>
        /// this will be used cache key
        /// </summary>
        /// <param name="clientRequest"></param>
        /// <returns></returns>
        public string Build(ClientRequest clientRequest)
        {
            return $"client-{clientRequest.ClientId}";
        }
    }
}
