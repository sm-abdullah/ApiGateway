using Microsoft.AspNetCore.Http;
namespace RateLimit
{
    public interface IClientResolver
    {
        string ResolveClient(HttpContext httpContext);
    }

}
