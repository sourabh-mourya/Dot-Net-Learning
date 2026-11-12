using ASPCoreWebAPICRUD.Data;
using ASPCoreWebAPICRUD.DTOs;
using ASPCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPICRUD.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentAPIController : ControllerBase
    {
        private readonly ApplicationDBContext applicationDB;

        public StudentAPIController(ApplicationDBContext applicationDB)
        {
            this.applicationDB = applicationDB;
        }

        [HttpGet("getStudent")]
        public async Task<ActionResult<List<Student>>> getStudent()
        {

            var data = await applicationDB.Students.ToListAsync();
            return Ok(data);
        }

        [HttpGet("getSingleStudent/{id}")]

        public async Task<ActionResult<List<Student>>> getSingleStudent(int id)
        {
            var data = await applicationDB.Students.FindAsync(id);
            if (data == null)
            {
                var ErrorMessage = "Student Is Not Found";
                return NotFound(ErrorMessage);
            }

            return Ok(data);

        }

        [HttpPost("CreateStudent")]
        public async Task<ActionResult<List<Student>>> CreateStudent(Student student)
        {
            await applicationDB.Students.AddAsync(student);
            await applicationDB.SaveChangesAsync();

            // return Ok(student);

            //like agar apne message bhi bhejna chahte hi to kaise kya bhejege 


            var data = "Student created successfully";

            return Ok(new
            {
                message = data,
                student = student
            });
        }

        [HttpPut("UpdateStudent/{id}")]
        //iss put request se to saara data update hoga agar apne ko particular koi field update krwani hi to kaise krwayege wo patch request se krawayege...
        public async Task<ActionResult<List<Student>>> UpdateStudents(int id, Student student)
        {
            var data = await applicationDB.Students.FindAsync(id);

            if (data == null)
            {
                return NotFound("Student not found");
            }

            data.Name = student.Name;
            data.Age = student.Age;
            data.Email = student.Email;
            data.Gender = student.Gender;

            await applicationDB.SaveChangesAsync();

            return Ok(new
            {
                message = "Student updated successfully",
                data = data
            });
        }


        [HttpPatch("Update/{id}")]
        public async Task<ActionResult<List<Student>>> UpdateStudent(int id, StudentUpdateDto student)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Student ID");
            }

            var data = await applicationDB.Students.FindAsync(id);

            if (data == null)
            {
                return NotFound("Student not found");
            }

            if (student.Name != null)
                data.Name = student.Name;

            if (student.Gender != null)
                data.Gender = student.Gender;

            if (student.Age.HasValue)
                data.Age = student.Age.Value;

            if (student.Email != null)
                data.Email = student.Email;

            await applicationDB.SaveChangesAsync();

            return Ok(new
            {
                message = "Student updated successfully",
                data = data
            });
        }


        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<List<Student>>> DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Student ID");
            }

            var data = await applicationDB.Students.FindAsync(id);

            if (data == null)
            {
                return NotFound("Student not found");
            }

            applicationDB.Students.Remove(data);
            await applicationDB.SaveChangesAsync();
            return Ok("Student is delete is successfully......");
        }

    }
}