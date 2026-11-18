
using Hybrid_Caching.Models;

namespace Hybrid_Caching.Service
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetData(string key);

        Task<bool> SetData<T>(
            string key,
            T value,
            DateTimeOffset expirationTime
        );

        Task RemoveData(string key);
    }
}
