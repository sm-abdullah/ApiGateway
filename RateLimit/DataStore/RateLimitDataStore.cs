using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace RateLimit
{
    /// <summary>
    /// In memory implementation of Cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RateLimitDataStore<T> : IRateLimitDataStore<T>
    {

        private readonly IMemoryCache _cache;
        public RateLimitDataStore(IMemoryCache cache)
        {
            _cache = cache;

        }

        public Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
        {

            return Task.FromResult(_cache.TryGetValue(id, out _));

        }

        public Task<T> GetItemAsync(string id, CancellationToken cancellationToken = default)
        {

            if (_cache.TryGetValue(id, out T stored))
            {
                return Task.FromResult(stored);
            }
            return Task.FromResult(default(T));
        }
        public Task RemoveItemAsync(string id, CancellationToken cancellationToken = default)
        {

            _cache.Remove(id);
            return Task.CompletedTask;

        }

        public Task SetItemAsync(string id, T entry, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default)
        {

            var options = new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove
            };

            if (expirationTime.HasValue)
            {
                options.SetAbsoluteExpiration(expirationTime.Value);
            }

            _cache.Set(id, entry, options);

            return Task.CompletedTask;
        }
    }
}
