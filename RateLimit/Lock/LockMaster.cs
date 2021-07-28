using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace RateLimit
{
    /// <summary>
    /// Maintains a lock by Key in order to avoid blocking all client request
    /// it can be opitmized by Pool mechanisim i.e instead of creating new entery and removing on exit 
    /// we can try to reuse the entires by getting them from a pool of semaphores once used put them back into pool
    /// for now considering this optimization out of scope
    /// </summary>
    public class LockMaster
    {
        private static ConcurrentDictionary<string, SemaphoreSlim> _locker;

        public LockMaster() 
        {
            _locker =new ConcurrentDictionary<string, SemaphoreSlim>(); 
        }
        public async Task<Locker> GetLockerAsync(string key)
        {
            var lockObject = _locker.GetOrAdd(key, new SemaphoreSlim(1,1));
            await lockObject.WaitAsync();
            return new Locker(key, lockObject, RemoveLock);
        }

        private void RemoveLock(string  key) 
        {
            _locker.TryRemove(key, out _); //remove to avoid keep growing
        }
    }

    public class Locker : IDisposable
    {
        public SemaphoreSlim Lock { get; private set; }
        private string _key;
        private Action<string> _action;
        public Locker(string key, SemaphoreSlim _lock, Action<string> action) 
        {
            _key = key;
            _action = action;
            Lock = _lock;
        }
        public void Dispose()
        {
            Lock.Release();
            _action(_key); //remove from dictionary
            Lock = null; // Let GC to collect Locker
        }
    }

}
