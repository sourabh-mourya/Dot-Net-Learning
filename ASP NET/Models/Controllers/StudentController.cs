
using Microsoft.AspNetCore.Mvc;
using ModelsInASPCore.Models;

namespace student.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {

            var students = new List<studentModel>
            {
              new studentModel{ rollNo = 1,Name = "Hello World",Gender = "M",Standard = 12},
              new studentModel{ rollNo = 1,Name = "Hello World",Gender = "M",Standard = 12},
              new studentModel{ rollNo = 1,Name = "Hello World",Gender = "M",Standard = 12},
              new studentModel{ rollNo = 1,Name = "Hello World",Gender = "M",Standard = 12},
              new studentModel{ rollNo = 1,Name = "Hello World",Gender = "M",Standard = 12}
            };

            ViewData["myStudentData"]=students;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
