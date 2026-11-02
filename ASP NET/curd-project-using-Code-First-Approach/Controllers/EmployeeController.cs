
using curd_project_using_Code_First_Approach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace curd_project_using_Code_First_Approach.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDBContext applicationDB;

        public EmployeeController(ApplicationDBContext applicationDB)
        {
            this.applicationDB = applicationDB;
        }

        // GET: Employee/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                applicationDB.EmployeeTable.Add(emp);

                await applicationDB.SaveChangesAsync();

                return RedirectToAction(nameof(GetEmployees));
            }

            return View(emp);
        }

        // GET: Employee/GetEmployees
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await applicationDB.EmployeeTable.ToListAsync();

            return View(employees);
        }

        // GET: Employee/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await applicationDB.EmployeeTable.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee emp)
        {
            if (id != emp.EmpId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(emp);
            }

            var existingEmployee = await applicationDB.EmployeeTable.FindAsync(id);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.EmpName = emp.EmpName;
            existingEmployee.EmpAge = emp.EmpAge;
            existingEmployee.EmpSalary = emp.EmpSalary;
            existingEmployee.EmpDesignation = emp.EmpDesignation;

            await applicationDB.SaveChangesAsync();

            return RedirectToAction(nameof(GetEmployees));
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await applicationDB.EmployeeTable.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            applicationDB.EmployeeTable.Remove(employee);

            await applicationDB.SaveChangesAsync();

            return RedirectToAction(nameof(GetEmployees));
        }
    }
}
