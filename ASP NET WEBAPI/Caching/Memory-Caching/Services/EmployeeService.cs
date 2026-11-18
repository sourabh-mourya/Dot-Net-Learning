
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public EmployeeService(
            ApplicationDbContext context,
            IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // Get all employees
        public async Task<List<Employee>> GetAllAsync()
        {
            var totalWatch = Stopwatch.StartNew();

            if (_cache.TryGetValue("employees", out List<Employee>? employees))
            {
                Console.WriteLine("[CACHE HIT] Employees found in memory cache.");

                totalWatch.Stop();

                Console.WriteLine($"[CACHE TIME] {totalWatch.ElapsedMilliseconds} ms");

                return employees!;
            }

            Console.WriteLine("[CACHE MISS] Employees not found in cache.");
            Console.WriteLine("[DATABASE] Fetching employees from MySQL...");

            var dbWatch = Stopwatch.StartNew();

            employees = await _context.Employees.ToListAsync();

            dbWatch.Stop();

            Console.WriteLine($"[DATABASE TIME] {dbWatch.ElapsedMilliseconds} ms");

            _cache.Set("employees", employees, TimeSpan.FromMinutes(5));

            Console.WriteLine("[CACHE SET] Employees stored in memory cache for 5 minutes.");

            totalWatch.Stop();

            Console.WriteLine($"[TOTAL TIME] {totalWatch.ElapsedMilliseconds} ms");

            return employees;
        }

        // Get employee by ID
        public async Task<Employee?> GetByIdAsync(int id)
        {
            var totalWatch = Stopwatch.StartNew();

            string key = $"employee_{id}";

            if (_cache.TryGetValue(key, out Employee? employee))
            {
                Console.WriteLine($"[CACHE HIT] Employee {id} found in memory cache.");

                totalWatch.Stop();

                Console.WriteLine($"[CACHE TIME] {totalWatch.ElapsedMilliseconds} ms");

                return employee;
            }

            Console.WriteLine($"[CACHE MISS] Employee {id} not found in cache.");
            Console.WriteLine($"[DATABASE] Fetching employee {id} from MySQL...");

            var dbWatch = Stopwatch.StartNew();

            employee = await _context.Employees.FindAsync(id);

            dbWatch.Stop();

            Console.WriteLine($"[DATABASE TIME] {dbWatch.ElapsedMilliseconds} ms");

            if (employee != null)
            {
                _cache.Set(key, employee, TimeSpan.FromMinutes(5));

                Console.WriteLine($"[CACHE SET] Employee {id} stored in memory cache.");
            }
            else
            {
                Console.WriteLine($"[DATABASE] Employee {id} not found.");
            }

            totalWatch.Stop();

            Console.WriteLine($"[TOTAL TIME] {totalWatch.ElapsedMilliseconds} ms");

            return employee;
        }

        // Update employee
        public async Task UpdateAsync(Employee emp)
        {
            Console.WriteLine($"[UPDATE] Updating employee {emp.Id}...");

            var watch = Stopwatch.StartNew();

            _context.Employees.Update(emp);

            await _context.SaveChangesAsync();

            watch.Stop();

            Console.WriteLine($"[UPDATE TIME] {watch.ElapsedMilliseconds} ms");

            _cache.Remove("employees");
            _cache.Remove($"employee_{emp.Id}");

            Console.WriteLine("[CACHE INVALIDATED] Employee list and individual employee cache removed.");
        }

        // Delete employee
        public async Task DeleteAsync(int id)
        {
            Console.WriteLine($"[DELETE] Deleting employee {id}...");

            var watch = Stopwatch.StartNew();

            var employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);

                await _context.SaveChangesAsync();

                _cache.Remove("employees");
                _cache.Remove($"employee_{id}");

                Console.WriteLine($"[CACHE INVALIDATED] Employee {id} cache removed.");
                Console.WriteLine("[DELETE] Employee deleted successfully.");
            }
            else
            {
                Console.WriteLine($"[DELETE] Employee {id} not found.");
            }

            watch.Stop();

            Console.WriteLine($"[DELETE TIME] {watch.ElapsedMilliseconds} ms");
        }
    }
}
