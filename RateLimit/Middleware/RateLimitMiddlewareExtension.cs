using Microsoft.AspNetCore.Builder;

namespace RateLimit.Middleware
{
    public static class RateLimitMiddlewareExtension
    {
        public static IApplicationBuilder UseRateLimitMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RateLimitMiddleware>();
        }
    }
}
