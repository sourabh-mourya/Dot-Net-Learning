
using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{   
    [Route("[Controller]/[Action]")]
    public class StudentController : Controller
    {
        // 2. Return a direct string
        [Route("~/")]
        public string Hello()
        {
            return "Hello, Student!";
        }

        // 3. Return an integer
        public int GetStudentCount()
        {
            return 100;
        }

        // 4. Return a list of students as JSON
        public IActionResult GetStudents()
        {
            var students = new List<string>
            {
                "Sourabh",
                "Rahul",
                "Aman",
                "Priya"
            };

            return Json(students);
        }

        // 5. Return an object as JSON
        public IActionResult GetStudent()
        {
            var student = new
            {
                Id = 1,
                Name = "Sourabh",
                Course = "Computer Science",
                Age = 21
            };

            return Ok(student);
        }

        // 6. Return NotFound response
        public IActionResult FindStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Student ID must be greater than 0.");
            }

            if (id != 1)
            {
                return NotFound("Student not found.");
            }

            return Ok(new
            {
                Id = 1,
                Name = "Sourabh"
            });
        }
    }
}
