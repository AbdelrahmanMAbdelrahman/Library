using Microsoft.Extensions.Caching.Hybrid;
using System.Runtime.Serialization;

namespace Library.Application.Common.Behaviors;

public sealed class CachingBehavior<TReq, TRes>(
    ILogger<CachingBehavior<TReq, TRes>> logger,
    HybridCache hybridCache
    ) : IPipelineBehavior<TReq, TRes> where TReq : notnull
{
    public async Task<TRes> Handle(TReq request, RequestHandlerDelegate<TRes> next, CancellationToken cancellationToken)
    {
        if(request is not ICachedQuery cachedRequest)
        {
           return await next(cancellationToken);
        }
        TRes res = await hybridCache.GetOrCreateAsync<TRes>(cachedRequest.Key,
             token=>new ValueTask<TRes>(next(token)) ,
            new HybridCacheEntryOptions
            {
               Expiration=cachedRequest.Expiration
            },
            cachedRequest.Tags
            ,
            cancellationToken
            );
        logger.LogWarning("+++++++++++++++++++++{res}++++++++++++++++++++", res);
        //if(res is null)
        //{
        //    res = await next(cancellationToken);
        //   await hybridCache.SetAsync(cachedRequest.Key,res,
        //       new HybridCacheEntryOptions
        //       {
        //           Flags=HybridCacheEntryFlags.DisableUnderlyingData
        //       },cachedRequest.Tags,cancellationToken);
        //}
        return res;
    }
}
