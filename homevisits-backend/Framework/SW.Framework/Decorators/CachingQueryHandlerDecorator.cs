using SW.Framework.Cqrs;
using System;

namespace SW.Framework.Decorators
{
    //public sealed class CachingQueryHandlerDecorator<TQuery, TResponse> : IQueryDecorator<TQuery, TResponse>
    //    where TQuery : class
    //{
    //    private readonly IMemoryCache _memoryCache;
    //    private readonly IQueryHandler<TQuery, TResponse> _decorated;

    //    public CachingQueryHandlerDecorator(IQueryHandler<TQuery, TResponse> decorated,
    //        IMemoryCache memoryCache)
    //    {
    //        _decorated = decorated;
    //        _memoryCache = memoryCache;
    //    }

    //    public TResponse Read(TQuery query)
    //    {
    //        TResponse cacheEntry;
    //        // Look for cache key.
    //        if (!_memoryCache.TryGetValue(query.GetHashCode(), out cacheEntry))
    //        {
    //            // Key not in cache, so get data.
    //            cacheEntry = _decorated.Read(query);

    //            // Set cache options.
    //            var cacheEntryOptions = new MemoryCacheEntryOptions()
    //                // Keep in cache for this time, reset time if accessed.
    //                .SetSlidingExpiration(TimeSpan.FromMinutes(3));

    //            // Save data in cache.
    //            _memoryCache.Set(query.GetHashCode(), cacheEntry, cacheEntryOptions);
    //        }

    //        return cacheEntry;
//}
//    }
}