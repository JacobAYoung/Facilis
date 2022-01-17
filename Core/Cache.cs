using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Core
{
    public class Cache : 
        ICache
    {
        private readonly ICacheEviction cacheEviction;

        private readonly IMemoryCache memoryCache;

        public Cache(ICacheEviction cacheEviction, IMemoryCache memoryCache)
        {
            this.cacheEviction = cacheEviction;
            this.memoryCache = memoryCache;
        }

        public TItem GetOrCreate<TItem>(object key, Func<ICacheEntry, TItem> factory, TimeSpan? expiration = null)
        {
            //Add the key to the eviction process
            cacheEviction.AddKeyToEvict(key.ToString());

            //Redis probably would've been a better solution but IMemoryCache isn't bad either
            return memoryCache.GetOrCreate(key, factory);
        }

        public Task<TItem> GetOrCreateAsync<TItem>(object key, Func<ICacheEntry, Task<TItem>> factory, TimeSpan? expiration = null)
        {
            //Add the key to the eviction process
            cacheEviction.AddKeyToEvict(key.ToString());

            //Redis probably would've been a better solution but IMemoryCache isn't bad either
            return memoryCache.GetOrCreateAsync(key, factory);
        }
    }
}
