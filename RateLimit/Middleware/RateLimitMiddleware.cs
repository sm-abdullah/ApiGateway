using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

using System.Threading.Tasks;

namespace RateLimit
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRateLimit _rateLimiter;
        public RateLimitMiddleware(RequestDelegate next, IRateLimit rateLimiter)
        {
            _next = next;
            _rateLimiter = rateLimiter;
        }
        public async Task Invoke(HttpContext context)
        {
            await _rateLimiter.HandleRequest(context, _next);
        }
    }

}
