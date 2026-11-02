using curd_project_using_Code_First_Approach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace curd_project_using_Code_First_Approach.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDBContext applicationDB;

        public StudentController(ApplicationDBContext applicationDB)
        {
            this.applicationDB = applicationDB;
        }

        // GET: Student/Create
        ///Iska purpose student registration form display karna hai.
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student std)
        {
            if (ModelState.IsValid)
            {
                applicationDB.StudentTable.Add(std);

                await applicationDB.SaveChangesAsync();

                return RedirectToAction("GetStudents");
            }

            return View(std);
        }

        // GET: Student/GetStudents
        [HttpGet]
        [Route("~/")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await applicationDB.StudentTable.ToListAsync();

            return View(students);
        }

        //  [HttpDelete] agar ye rkho to fir postman se testing krna url + method dono ka naam dekar 
        [HttpPost] //abhi me browser se krunga to isliye ye rkh kr rkha hi mene 


        public async Task<IActionResult> Delete(int? id)
        {
            var student = await applicationDB.StudentTable
         .FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            applicationDB.StudentTable.Remove(student);

            await applicationDB.SaveChangesAsync();

            return RedirectToAction("GetStudents");
        }
    }
}