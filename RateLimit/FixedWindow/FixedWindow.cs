using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace RateLimit
{
    public class FixedWindow : IRateLimit
    {
        private LockMaster _lockManager;
        private IRateLimitDataStore<RequestCounter> _dataStore; // can have different implementation like i implemented In Memory solution, can be sclaed distributed like redis
        private IRateLimitSettingManager _settingManager;
        private IRulesManager _rulesManager;

        public FixedWindow(IRateLimitDataStore<RequestCounter> dataStore, IRateLimitSettingManager settingManager, IRulesManager rulesManager) 
        {
            _dataStore = dataStore;
            _lockManager = new LockMaster();
            _settingManager = settingManager;
            _rulesManager = rulesManager;
        }
        public async Task HandleRequest(HttpContext context, RequestDelegate next)
        {
            var clientRequest = ResolveRequest(context);
            var rule = _rulesManager.GetMatchingRule(clientRequest);
            if (rule != null) 
            {
                using (var locker = await _lockManager.GetLockerAsync(rule.Key))
                {
                    var requestCounter = await _dataStore.GetItemAsync(rule.Key);
                    if (requestCounter != null) 
                    {
                        if (requestCounter.Timestamp + rule.TimeSpan < DateTime.UtcNow)
                        {
                            await _dataStore.SetItemAsync(rule.Key, new RequestCounter { Count = 1, Timestamp = DateTime.UtcNow }, rule.TimeSpan, context.RequestAborted);
                        }
                        if (requestCounter.Count + 1 > rule.Count)
                        {
                            context.Response.StatusCode = _settingManager.RateLimitSettings.HttpStatusCode;
                            await context.Response.WriteAsync(string.Format(_settingManager.RateLimitSettings.QuotaExceededResponse.Content, requestCounter.Timestamp.RetryAfterFrom(rule.TimeSpan)));
                            return;
                        }
                        else if (requestCounter.Timestamp + rule.TimeSpan >= DateTime.UtcNow)
                        {
                            await _dataStore.SetItemAsync(rule.Key, new RequestCounter { Count = requestCounter.Count + 1, Timestamp = requestCounter.Timestamp }, rule.TimeSpan, context.RequestAborted);
                        }
                       
                    }
                    else 
                    {
                        await _dataStore.SetItemAsync(rule.Key, new RequestCounter { Count = 1, Timestamp = DateTime.UtcNow }, rule.TimeSpan, context.RequestAborted);
                    }

                    await next(context);
                }
            }
           

            
        }


        private  ClientRequest ResolveRequest(HttpContext httpContext)
        {
            return new ClientRequest
            {
                ClientId = _settingManager.ClientResolver.ResolveClient(httpContext) ?? "anonymous",
                UrlPath = httpContext.Request.Path.ToString().ToLowerInvariant().TrimEnd('/'),
                Verb = httpContext.Request.Method.ToLowerInvariant(),
            };
        }
    }
}
