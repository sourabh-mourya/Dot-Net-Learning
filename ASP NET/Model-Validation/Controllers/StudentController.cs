using Microsoft.AspNetCore.Mvc;
using MyASPCoreApp.Models;

namespace MyASPCoreApp.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                // Save student into database

                return Content("Student Created Successfully");
            }

            return View(student);
        }
    }
}