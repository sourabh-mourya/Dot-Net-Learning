using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{

    [Route("[Controller]/[Action]")]
    public class HomeController : Controller
    {
        [Route("~/")] //By default when url open these view is return ...

        public IActionResult Hello()
        {
            return View();
        }

        public IActionResult Calculation()
        {
            return View();
        }
    }
}