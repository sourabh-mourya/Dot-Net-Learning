
using Distrubute_Caching_Redis_.Cache;
using Distrubute_Caching_Redis_.Data;
using Distrubute_Caching_Redis_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Distrubute_Caching_Redis_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly ApplicationDbContext _dbContext;

        public EmployeeController(
            ICacheService cacheService,
            ApplicationDbContext dbContext)
        {
            _cacheService = cacheService;
            _dbContext = dbContext;
        }

        // 1. GET ALL EMPLOYEES
        // GET: api/Employee/Employees
        [HttpGet("Employees")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Employee>? cachedData;

            // Get data from Redis
            cachedData = await _cacheService
                .GetData<IEnumerable<Employee>>("Employees");

            // Cache HIT
            if (cachedData != null)
            {
                return Ok(cachedData);
            }

            // Cache MISS: Get data from Database
            cachedData = await _dbContext.Employees.ToListAsync();

            // Set expiration time
            var expirationTime = DateTimeOffset.UtcNow.AddMinutes(5);

            // Store data in Redis
            await _cacheService.SetData(
                "Employees",
                cachedData,
                expirationTime
            );

            return Ok(cachedData);
        }

        // 2. GET EMPLOYEE BY ID
        // GET: api/Employee/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Employee? employee;

            string cacheKey = $"Employee_{id}";

            // Get data from Redis
            employee = await _cacheService
                .GetData<Employee>(cacheKey);

            // Cache HIT
            if (employee != null)
            {
                return Ok(employee);
            }

            // Cache MISS: Get data from Database
            employee = await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            // Set expiration time
            var expirationTime = DateTimeOffset.UtcNow.AddMinutes(5);

            // Store data in Redis
            await _cacheService.SetData(
                cacheKey,
                employee,
                expirationTime
            );

            return Ok(employee);
        }

        // 3. CREATE EMPLOYEE
        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            // Add employee to Database
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();

            // Invalidate Employees list cache
            await _cacheService.RemoveData("Employees");

            return CreatedAtAction(
                nameof(GetById),
                new { id = employee.Id },
                employee
            );
        }

        // 4. UPDATE EMPLOYEE
        // PUT: api/Employee/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("Employee ID mismatch.");
            }

            // Check employee in Database
            var existingEmployee = await _dbContext.Employees
                .FindAsync(id);

            if (existingEmployee == null)
            {
                return NotFound("Employee not found.");
            }

            // Update employee properties
            _dbContext.Entry(existingEmployee).CurrentValues
                .SetValues(employee);

            await _dbContext.SaveChangesAsync();

            // Invalidate old cached data
            await _cacheService.RemoveData("Employees");
            await _cacheService.RemoveData($"Employee_{id}");

            return NoContent();
        }

        // 5. DELETE EMPLOYEE
        // DELETE: api/Employee/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Find employee in Database
            var employee = await _dbContext.Employees
                .FindAsync(id);

            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            // Delete employee
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            // Invalidate cached data
            await _cacheService.RemoveData("Employees");
            await _cacheService.RemoveData($"Employee_{id}");

            return NoContent();
        }
    }
}
