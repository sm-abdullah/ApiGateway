
namespace RateLimit
{
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
