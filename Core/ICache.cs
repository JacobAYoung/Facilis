using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// ICache is meant to be the wrapper between the main code and the actual MemoryCache code. 
    /// We want to use this class as a way to add in our own logic in between the calls for caching. I.E. The evicition process.
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Get or create the cached item
        /// </summary>
        /// <typeparam name="TItem">The type to be returned</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="factory">Code to run during cache</param>
        /// <param name="expiration">Expiration time of the cached results. Currently not in use.</param>
        /// <returns>Runs the factory code and returns the results</returns>
        TItem GetOrCreate<TItem>(object key, Func<ICacheEntry, TItem> factory, TimeSpan? expiration = null);

        /// <summary>
        /// Get or create the cached item while being async
        /// </summary>
        /// <typeparam name="TItem">The type to be returned</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="factory">Code to run during cache</param>
        /// <param name="expiration">Expiration time of the cached results. Currently not in use.</param>
        /// <returns>Runs the factory code and returns the results</returns>
        Task<TItem> GetOrCreateAsync<TItem>(object key, Func<ICacheEntry, Task<TItem>> factory, TimeSpan? expiration = null);
    }
}
