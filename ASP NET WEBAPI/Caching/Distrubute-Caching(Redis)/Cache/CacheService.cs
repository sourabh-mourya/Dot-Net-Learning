
using StackExchange.Redis;
using System.Text.Json;

namespace Distrubute_Caching_Redis_.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase redisDb;

        public CacheService()
        {
            redisDb = ConnectionHelper.Connection.GetDatabase();
        }

        // Get data from Redis
        public async Task<T?> GetData<T>(string key)
        {
            var value = await redisDb.StringGetAsync(key);

            if (value.IsNullOrEmpty)
            {
                Console.WriteLine($"[REDIS MISS] Key: {key}");
                return default;
            }

            Console.WriteLine($"[REDIS HIT] Key: {key}");

            return JsonSerializer.Deserialize<T>(value.ToString());
        }

        // Set data into Redis
        public async Task<bool> SetData<T>(
            string key,
            T value,
            DateTimeOffset expirationTime)
        {
            var serializedData = JsonSerializer.Serialize(value);

            var expiration = expirationTime - DateTimeOffset.UtcNow;

            if (expiration <= TimeSpan.Zero)
            {
                Console.WriteLine("[REDIS] Expiration time must be in the future.");
                return false;
            }

            bool result = await redisDb.StringSetAsync(
                key,
                serializedData,
                expiration
            );

            Console.WriteLine($"[REDIS SET] Key: {key}, Result: {result}");

            return result;
        }

        // Remove data from Redis
        public async Task<bool> RemoveData(string key)
        {
            bool result = await redisDb.KeyDeleteAsync(key);

            Console.WriteLine($"[REDIS DELETE] Key: {key}, Result: {result}");

            return result;
        }
    }
}
