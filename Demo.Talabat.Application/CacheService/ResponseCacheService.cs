using Demo.Talabat.Core.Services.Contract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.Talabat.Application.CacheService
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase database;
        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            database=redis.GetDatabase();   
        }

        public async Task CacheResponseAsync(string key, object Response, TimeSpan timeToLive)
        {
            if (Response == null) return;

            var serializeOptions=new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var serializedResponse = JsonSerializer.Serialize(Response, serializeOptions);
            await database.StringSetAsync(key, serializedResponse,timeToLive);
        }

        public async Task<string?> GetCachedResponseAsync(string key)
        {
            var response=await database.StringGetAsync(key);


            if(response.IsNullOrEmpty) return null;
            return response;
        }
    }
}
