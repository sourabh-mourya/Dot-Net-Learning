using Hybrid_Caching.Data;
using Hybrid_Caching.Models;
using Hybrid_Caching.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hybrid_Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // Saare employees ki list is ek key me cache hoti hai
        private const string EmployeesKey = "Employees";

        private readonly IEmployeeService _employeeService;
        private readonly ApplicationDbContext _dbContext;

        public EmployeeController(
            IEmployeeService employeeService,
            ApplicationDbContext dbContext)
        {
            _employeeService = employeeService;
            _dbContext = dbContext;
        }

        // 1. GET ALL EMPLOYEES
        // GET: api/Employee
        // Flow: Memory -> Redis -> MySQL (console me [MEMORY HIT] / [REDIS HIT] dikhega)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetData(EmployeesKey);
            return Ok(employees);
        }

        // 2. GET EMPLOYEE BY ID
        // GET: api/Employee/1
        // Cached list se hi filter hota hai, isliye alag DB query nahi lagti
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employees = await _employeeService.GetData(EmployeesKey);

            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            return Ok(employee);
        }

        // 3. CREATE EMPLOYEE
        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();

            // List purani ho gayi: Memory + Redis dono se hatao
            await _employeeService.RemoveData(EmployeesKey);

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

            var existingEmployee = await _dbContext.Employees.FindAsync(id);

            if (existingEmployee == null)
            {
                return NotFound("Employee not found.");
            }

            _dbContext.Entry(existingEmployee).CurrentValues.SetValues(employee);
            await _dbContext.SaveChangesAsync();

            // Purana cache hatao
            await _employeeService.RemoveData(EmployeesKey);

            return NoContent();
        }

        // 5. DELETE EMPLOYEE
        // DELETE: api/Employee/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            // Purana cache hatao
            await _employeeService.RemoveData(EmployeesKey);

            return NoContent();
        }

        // 6. CLEAR CACHE (testing ke liye)
        // DELETE: api/Employee/cache
        [HttpDelete("cache")]
        public async Task<IActionResult> ClearCache()
        {
            await _employeeService.RemoveData(EmployeesKey);
            return NoContent();
        }
    }
}