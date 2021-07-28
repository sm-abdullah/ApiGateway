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
        //public class MaxConcurrentRequestsMiddleware
        //{
        //    private int _concurrentRequestsCount;

        //    private readonly RequestDelegate _next;
        //    private readonly MaxConcurrentRequestsOptions _options;

        //    public MaxConcurrentRequestsMiddleware(RequestDelegate next,
        //        IOptions<MaxConcurrentRequestsOptions> options)
        //    {
        //        _concurrentRequestsCount = 0;

        //        _next = next ?? throw new ArgumentNullException(nameof(next));
        //        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        //    }

        //    public async Task Invoke(HttpContext context)
        //    {
        //        if (CheckLimitExceeded())
        //        {
        //            IHttpResponseFeature responseFeature = context.Features.Get<IHttpResponseFeature>();

        //            responseFeature.StatusCode = StatusCodes.Status503ServiceUnavailable;
        //            responseFeature.ReasonPhrase = "Concurrent request limit exceeded.";
        //        }
        //        else
        //        {
        //            await _next(context);

        //            // TODO: Decrement concurrent requests count
        //        }
        //    }

        //    private bool CheckLimitExceeded()
        //    {
        //        bool limitExceeded = false;

        //        // TODO: Check and increment concurrent requests count

        //        return limitExceeded;
        //    }
        //}
    }
