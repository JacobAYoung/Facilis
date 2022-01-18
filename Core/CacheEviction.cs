using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public class CacheEviction : ICacheEviction
    {
        private readonly IMemoryCache memoryCache;

        //Really the value could've been anything. I figured I would make it a nullable timespan for future. 
        //Just incase anyone would want to set current cache eviction timers based off of that.
        private ConcurrentDictionary<string, TimeSpan?> CacheKeysToEvict = new ConcurrentDictionary<string, TimeSpan?>();

        public CacheEviction(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            StartTimer();
        }

        private async void StartTimer()
        {
            //Wait till the nearest hour
            int secondsTillHour = (int)(3600 - DateTime.Now.TimeOfDay.TotalSeconds % 3600);
            await Task.Delay(secondsTillHour * 60);

            //Start the timer to reset the cache every 15 minutes
            Timer t = new Timer(ResetCacheOnTimer, null, 0, 900000);
        }

        private void ResetCacheOnTimer(Object o)
        {
            Parallel.ForEach(CacheKeysToEvict.Keys, key =>
            {
                this.memoryCache.Remove(key);
            });
        }

        public void AddKeyToEvict(string key, TimeSpan? timer = null)
        {
            CacheKeysToEvict.TryAdd(key, timer);
        }
    }
}
