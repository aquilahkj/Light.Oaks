using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;

namespace Light.Oaks
{
    class RedisCacheAgent : ICacheAgent
    {
        readonly RedisCache _client;
        readonly string prefix;

        public RedisCacheAgent(string prefix, string redisConfig)
        {
            this.prefix = prefix;
            if (string.IsNullOrWhiteSpace(redisConfig)) {
                redisConfig = "127.0.0.1:6379,abortConnect=false";
            }
            _client = new RedisCache(new RedisCacheOptions() {
                Configuration = redisConfig
            });
        }

        public string GetCache(string key)
        {
            var pkey = prefix + key;
            var data = _client.GetString(prefix + key);
            _client.Refresh(pkey);
            return data;
        }

        public void RemoveCache(string key)
        {
            _client.Remove(prefix + key);
        }

        public void SetCache(string key, string value, TimeSpan expiry)
        {
            _client.SetString(prefix + key, value, new DistributedCacheEntryOptions() {
                SlidingExpiration = expiry
            });
        }
    }
}
