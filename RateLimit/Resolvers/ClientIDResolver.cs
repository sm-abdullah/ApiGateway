using Microsoft.AspNetCore.Http;
using System.Linq;


namespace RateLimit
{
    public class ClientIDResolver : IClientResolver
    {
        private readonly string _clientIdHeader;

        public ClientIDResolver(string clientIdHeader)
        {
            _clientIdHeader = clientIdHeader;
        }
        public string ResolveClient(HttpContext httpContext)
        {
            string clientId = null;

            if (httpContext.Request.Headers.TryGetValue(_clientIdHeader, out var values))
            {
                clientId = values.First();
            }
            return clientId;
        }
    }
}
