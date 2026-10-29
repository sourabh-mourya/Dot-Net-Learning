using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{

    [Route("[Controller]/[Action]")]
    public class HomeController : Controller
    {
        [Route("~/")] //By default when url open these view is return ...

        public IActionResult Hello()
        {
            ViewData["data1"] = "Hello world";
            ViewData["data2"] = 23;
            ViewData["data3"] = DateTime.Now.ToLongDateString();

            string[] arr = {"Hello ","World","To","This"};
            ViewData["data4"]=arr;
            return View();
        }

        public IActionResult Calculation()
        {

            ViewBag.data1="Passing data to view through ViewBag";
            ViewBag.data2=225;
            string[] arr1={"View","Bag","Data"};
            ViewBag.data3=arr1; 
            return View();
        }
    }
}