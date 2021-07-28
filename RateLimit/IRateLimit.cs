using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace RateLimit
{
    public interface IRateLimit
    {
         Task HandleRequest(HttpContext context, RequestDelegate next);
    }
}
