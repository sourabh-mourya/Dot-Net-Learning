
using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        // GET: /Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/getMethod
        [HttpGet]
        public int getMethod()
        {
            return 54;
        }

        // POST: /Home/postMethod
        [HttpPost]
        public IActionResult postMethod()
        {
            return Content("Post Method");
        }
    }
}
