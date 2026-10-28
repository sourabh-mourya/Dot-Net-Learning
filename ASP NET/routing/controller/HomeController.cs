
using Microsoft.AspNetCore.Mvc;


namespace MyMvcApp.Controllers
{

    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

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