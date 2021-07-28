
namespace RateLimit
{
    public class ClientRequest
    {
        public string ClientId { get; set; }

        // below members can be used to implment different strategies
        // like ip based rate limiting 
        // or infact having specific limit for each endpoint even with verbe PUT|GET|POST
        public string IpAddress { get; set; }
        public string UrlPath { get; set; }
        public string Verb { get; set; }
    }
}
