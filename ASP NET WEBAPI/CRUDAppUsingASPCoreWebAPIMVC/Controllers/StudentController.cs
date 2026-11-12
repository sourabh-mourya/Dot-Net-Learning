using System.Text;
using CRUDAppUsingASPCoreWebAPIMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRUDAppUsingASPCoreWebAPIMVC.Controllers
{
    public class StudentController : Controller
    {
        private const string BaseUrl = "http://localhost:5053/api/StudentAPI/";

        private readonly string url = BaseUrl + "getStudent/";
        private readonly string postUrl = BaseUrl + "CreateStudent/";
        private readonly string singleStudent = BaseUrl + "getSingleStudent/";
        private readonly string updateSingleStudent = BaseUrl + "UpdateStudent/";
        private readonly string deleteStudent = BaseUrl + "delete/";

        private static readonly HttpClient client = new HttpClient();

        // ================= INDEX (GET LIST) =================
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Step 1: Pehle ek khali list banao (isme API ka data aayega)
            List<Student> students = new List<Student>();

            try
            {
                // Step 2: API ko GET request bhejo
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Step 3: Response body ko JSON string me padho
                    string jsonString = await response.Content.ReadAsStringAsync();

                    // Step 4: JSON string ko List<Student> me convert (Deserialize) karo
                    List<Student>? apiData = JsonConvert.DeserializeObject<List<Student>>(jsonString);

                    // Step 5: Agar data mila (null nahi hai) to apni list me daalo
                    if (apiData != null)
                    {
                        students = apiData;
                    }
                }
                else
                {
                    ViewBag.Error = "API Error: " + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            ViewBag.StudentCount = students.Count;

            // Step 6: List ko View me bhejo
            return View(students);
        }

        // ================= DETAILS (GET SINGLE) =================
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Student? student = await GetStudentById(id);

            if (student == null)
            {
                TempData["Error_Message"] = "Student not found";
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // ================= CREATE =================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student std)
        {
            // Step 1: Form se aaye data se ek naya Student object (variable) banao
            Student newStudent = new Student();
            newStudent.Name = std.Name;
            newStudent.Gender = std.Gender;
            newStudent.Age = std.Age;
            newStudent.Email = std.Email;
            newStudent.Password = std.Password;

            // Step 2: Us variable ko JSON string me convert (Serialize) karo
            string jsonString = JsonConvert.SerializeObject(newStudent);

            // Step 3: JSON string ko HTTP body (content) me daalo
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // Step 4: API ko POST request bhejo
            HttpResponseMessage response = await client.PostAsync(postUrl, content);

            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Student added successfully....";
                return RedirectToAction("Index");
            }

            // Fail hua to API ka error body padho aur form wapas dikhao
            string errorBody = await response.Content.ReadAsStringAsync();
            ViewBag.Error = "API Error: " + response.StatusCode + " - " + errorBody;
            return View(std);
        }

        // ================= EDIT =================
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Student? student = await GetStudentById(id);

            if (student == null)
            {
                TempData["Error_Message"] = "Student not found";
                return RedirectToAction("Index");
            }

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student std)
        {
            // Step 1: Form se aaye data se Student object (variable) banao
            Student updatedStudent = new Student();
            updatedStudent.Id = std.Id;
            updatedStudent.Name = std.Name;
            updatedStudent.Gender = std.Gender;
            updatedStudent.Age = std.Age;
            updatedStudent.Email = std.Email;

            // API ke [Required] Password validation ke liye placeholder
            // (UpdateStudents action Password ko touch nahi karta, to DB me purana password safe rahega)
            updatedStudent.Password = string.IsNullOrEmpty(std.Password) ? "placeholder" : std.Password;

            // Step 2: Variable ko JSON string me convert (Serialize) karo
            string jsonString = JsonConvert.SerializeObject(updatedStudent);

            // Step 3: JSON ko HTTP content me daalo
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // Step 4: API ko PUT request bhejo (URL me id)
            HttpResponseMessage response = await client.PutAsync(updateSingleStudent + updatedStudent.Id, content);

            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Student updated successfully....";
                return RedirectToAction("Index");
            }

            string errorBody = await response.Content.ReadAsStringAsync();
            ViewBag.Error = "API Error: " + response.StatusCode + " - " + errorBody;
            return View(std);
        }

        // ================= DELETE =================
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Student? student = await GetStudentById(id);

            if (student == null)
            {
                TempData["Error_Message"] = "Student not found";
                return RedirectToAction("Index");
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // DELETE me body nahi hoti, sirf URL me id jaati hai
            HttpResponseMessage response = await client.DeleteAsync(deleteStudent + id);

            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Student deleted successfully....";
                return RedirectToAction("Index");
            }

            string errorBody = await response.Content.ReadAsStringAsync();
            ViewBag.Error = "API Error: " + response.StatusCode + " - " + errorBody;

            Student? student = await GetStudentById(id);
            return View("Delete", student ?? new Student { Id = id });
        }

        // ================= HELPER: GET SINGLE STUDENT =================
        private async Task<Student?> GetStudentById(int id)
        {
            // Step 1: Pehle ek variable banao (abhi null, kyunki data nahi mila)
            Student? student = null;

            try
            {
                // Step 2: API ko GET request bhejo
                HttpResponseMessage response = await client.GetAsync(singleStudent + id);

                if (response.IsSuccessStatusCode)
                {
                    // Step 3: Response ko JSON string me padho
                    string jsonString = await response.Content.ReadAsStringAsync();

                    // Step 4: JSON ko Student object me convert (Deserialize) karo
                    Student? apiData = JsonConvert.DeserializeObject<Student>(jsonString);

                    // Step 5: Mila to apne variable me daalo
                    if (apiData != null)
                    {
                        student = apiData;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetStudentById Error: " + ex.Message);
            }

            // Step 6: Student ya null return hoga
            return student;
        }
    }
}