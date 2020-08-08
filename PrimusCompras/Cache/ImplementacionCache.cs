using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace Cache
{
    public class ImplementacionCache : ICache
    {

        private readonly IMemoryCache _cache;
        const string UserKey = "Users";
        private readonly IDistributedCache _redisCache;
        private readonly ConfigKeys _config;

        public ImplementacionCache(IMemoryCache cacheMemory, IDistributedCache distributedCache, IOptions<ConfigKeys> config)
        {
            _cache = cacheMemory;
            _redisCache = distributedCache;
            _config = config.Value;
        }

        public async void MemoryCache()
        {
            if (!_cache.TryGetValue(UserKey, out UserResponse result))
            {
                using (var httpClient = new HttpClient())
                {
                    string apiResult = await httpClient.GetStringAsync(_config.requestUrl);
                    result = JsonConvert.DeserializeObject<UserResponse>(apiResult);

                    if (result != null)
                    {
                        var cacheExpirationsOptions = new MemoryCacheEntryOptions()
                        {
                            AbsoluteExpiration = DateTime.Now.AddHours(1),
                            Priority = CacheItemPriority.Normal,
                            SlidingExpiration = TimeSpan.FromMinutes(5)
                        };

                        _cache.Set(UserKey, result, cacheExpirationsOptions);
                    }

                }
            }
        }

        public async void RedisCache()
        {
            var encUsers = await _redisCache.GetAsync(UserKey);
            UserResponse result = null;

            if (encUsers != null)
            {
                result = JsonConvert.DeserializeObject<UserResponse>(Encoding.UTF8.GetString(encUsers));
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    string apiResult = await httpClient.GetStringAsync(_config.requestUrl);
                    result = JsonConvert.DeserializeObject<UserResponse>(apiResult);

                    if (result != null)
                    {
                        var cacheExpirationsOptions = new DistributedCacheEntryOptions()
                        {
                            AbsoluteExpiration = DateTime.Now.AddHours(1)
                        };

                        await _redisCache.SetStringAsync(UserKey, apiResult, cacheExpirationsOptions);
                    }
                }
            }
        }
    }
}
