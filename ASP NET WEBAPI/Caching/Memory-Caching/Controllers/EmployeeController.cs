using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _service.GetAllAsync();

            return Ok(employees);
        }

        // GET: api/Employees/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetByIdAsync(id);

            if (employee == null)
                return NotFound("Employee not found");

            return Ok(employee);
        }

        // PUT: api/Employees/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee emp)
        {
            if (id != emp.Id)
                return BadRequest("Invalid Employee ID");

            var employee = await _service.GetByIdAsync(id);

            if (employee == null)
                return NotFound("Employee not found");

            await _service.UpdateAsync(emp);

            return Ok("Employee updated successfully");
        }

        // DELETE: api/Employees/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _service.GetByIdAsync(id);

            if (employee == null)
                return NotFound("Employee not found");

            await _service.DeleteAsync(id);

            return Ok("Employee deleted successfully");
        }
    }
}









