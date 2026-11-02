using Microsoft.AspNetCore.Mvc;
using ModelsInASPCore.Repository;

namespace ModelsInASPCore.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent _studentService;

        public StudentController(IStudent studentService)
        {
            _studentService = studentService;
        }

        // MVC View
        [HttpGet("/Student/Index")]
        public IActionResult Index()
        {
            var students = _studentService.GetAllStudents();

            ViewData["data1"] = students;

            return View();
        }

        // API: Get all students
        [HttpGet("api/Student/GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            var students = _studentService.GetAllStudents();

            return Ok(students);
        }

        // API: Get student by ID
        [HttpGet("api/Student/GetStudent/{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = _studentService.GetStudentById(id);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            return Ok(student);
        }
    }
}