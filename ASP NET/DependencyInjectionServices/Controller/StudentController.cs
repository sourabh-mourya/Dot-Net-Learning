using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DependencyInjectionServices.Services;

namespace DependencyInjectionServices.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class StudentController : Controller
    {
        //jo apne interface banaya hi uska ek me variable banuga readonly private naam ka 

        private readonly IStudentService _studentService;

        //ab mene ye variable hi to iske ander me mujhe dependency ko inject krni hi ...

        //Constructor ke ander inject krunga isko ...


        public StudentController(IStudentService stdService)
        {//ab IStudentService ko intilize kr dunga...
            _studentService = stdService;
        }
        //student controller IStudentService ki jo StudentService Class hi wo Data providde kr rhi hi ..


        //jb me studentController ko run krunga 

        //to usko IstudentService chiye 
        //to wo p rogram .cs file me dekhege jakar hi ye service register hui hi ki nhi 
        ///then wo uss reigster Services ke corressponding ki konsi class register hui then wo usko call kregi...

        ///     to iski karan se ye object create nhi krega to dependency injection ke karan se 

        [HttpGet]
        public IActionResult GetStudent()
        {
            var name = _studentService.GetStudentName();
            var age = _studentService.GetStudentAge();

            return Ok(
                new
                {
                    Name = name,
                    Age = age
                }
            );
        }

        //jb me controller run krunga to wo service register krege automatic and dependecny ke karan unka data lekar aa jyega 
        // then jo mene variable create _studentService ka to intliaze ho jyega 

        //unse data lekar me response generate krwa dunga.......



    }
}