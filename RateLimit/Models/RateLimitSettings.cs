namespace RateLimit
{
  
    public class RateLimitSettings
    {
        public string ClientIdHeader { get; set; }
        public int HttpStatusCode { get; set; }
        public QuotaExceededResponse QuotaExceededResponse { get; set; }
    }
}
