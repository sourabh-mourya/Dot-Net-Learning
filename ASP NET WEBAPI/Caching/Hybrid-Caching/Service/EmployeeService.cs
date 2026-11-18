
using Hybrid_Caching.Data;
using Hybrid_Caching.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace Hybrid_Caching.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HybridCache _hybridCache;
        private readonly ApplicationDbContext _dbContext;

        public EmployeeService(
            HybridCache hybridCache,
            ApplicationDbContext dbContext)
        {
            _hybridCache = hybridCache;
            _dbContext = dbContext;
        }

        // Get all employees using Hybrid Cache
        public async Task<List<Employee>> GetData(string key)
        {
            return await _hybridCache.GetOrCreateAsync(
                key,
                async cancellationToken =>
                {
                    Console.WriteLine("[CACHE MISS] Fetching from MySQL");

                    return await _dbContext.Employees
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
                },
                new HybridCacheEntryOptions
                {
                    Expiration = TimeSpan.FromMinutes(5),
                    LocalCacheExpiration = TimeSpan.FromMinutes(2)
                }
            );
        }

        // Set data in Hybrid Cache
        public async Task<bool> SetData<T>(
            string key,
            T value,
            DateTimeOffset expirationTime)
        {
            var expiration = expirationTime - DateTimeOffset.UtcNow;

            if (expiration <= TimeSpan.Zero)
                return false;

            await _hybridCache.SetAsync(
                key,
                value,
                new HybridCacheEntryOptions
                {
                    Expiration = expiration,
                    LocalCacheExpiration = TimeSpan.FromMinutes(2)
                }
            );

            return true;
        }

        // Remove data from Hybrid Cache
        public async Task RemoveData(string key)
        {
            await _hybridCache.RemoveAsync(key);

            Console.WriteLine($"[HYBRID CACHE REMOVE] {key}");
        }
    }
}
