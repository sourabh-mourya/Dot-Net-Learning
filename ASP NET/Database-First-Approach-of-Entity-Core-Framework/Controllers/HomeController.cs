using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Database_First_Approach_of_Entity_Core_Framework.Models;
using Database_First_Approach_of_Entity_Core_Framework.Data;

namespace Database_First_Approach_of_Entity_Core_Framework.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDBContext applicationDB;
    public HomeController(ApplicationDBContext applicationDB)
    {
        this.applicationDB=applicationDB;
    }

    public IActionResult Index()
    {
        var data=applicationDB.Students.ToList();
        return View(data);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
