
using Microsoft.AspNetCore.Mvc;


namespace MyMvcApp.Controllers
{

    [Route("[Controller]/[Action]")]
    public class HomeController : Controller
    {

        // [Route("")]
        // [Route("/Home/Index1")]
        public IActionResult Index()
        {
            return View();
        }

        // [Route("/Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        public int Details(int id)
        {
            return id;
        }

        public string Hello()
        {
            return "Hello Action are calling and there return type are string .....";
        }
    }
}