
using System;
using System.Threading;
using System.Threading.Tasks;
namespace RateLimit
{
    /// <summary> 
    /// Creating async based so it can have distributed implementation like redis 
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRateLimitDataStore<T>
    {
        Task<bool> ExistsAsync(string id, CancellationToken cancellationToken =default);
        Task<T> GetItemAsync(string id, CancellationToken cancellationToken = default);
        Task RemoveItemAsync(string id, CancellationToken cancellationToken = default);
        Task SetItemAsync(string id, T entry, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default);
    }
}
