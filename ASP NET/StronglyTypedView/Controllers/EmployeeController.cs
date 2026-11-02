using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StronglyTypedView.Models;

namespace StronglyTypedView.Controllers;

public class EmployeeController : Controller
{
    public IActionResult Index()
    {
        Employee obj =  new Employee()
        {
          EmpId = 101,
          EmpName = "Umair",
          Designation = "Manager",
          Salary = 25000  
        };
        return View(obj);
    }

    public IActionResult Privacy()
    {
        var myEmployees = new List<Employee>
        {
            new Employee {EmpId = 101,EmpName = "Sourabh",Designation="Manager",Salary=25000},
            new Employee {EmpId = 101,EmpName = "Sourabh",Designation="Manager",Salary=25000},
            new Employee {EmpId = 101,EmpName = "Sourabh",Designation="Manager",Salary=25000},
            new Employee {EmpId = 101,EmpName = "Sourabh",Designation="Manager",Salary=25000},
            new Employee {EmpId = 101,EmpName = "Sourabh",Designation="Manager",Salary=25000},
            new Employee {EmpId = 101,EmpName = "Sourabh",Designation="Manager",Salary=25000}
        };
        return View(myEmployees);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
