namespace RateLimit
{
  
    public class RateLimitSettings
    {
        public string ClientIdHeader { get; set; }
        public string HttpStatusCode { get; set; }
        public QuotaExceededResponse QuotaExceededResponse { get; set; }
    }
}
