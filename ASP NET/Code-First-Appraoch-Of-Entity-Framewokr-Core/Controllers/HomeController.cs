using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Code_First_Appraoch_Of_Entity_Framewokr_Core.Models;

namespace Code_First_Appraoch_Of_Entity_Framewokr_Core.Controllers;

public class HomeController : Controller
{
    private readonly StudentDBContext studentDB;
    
    public HomeController(StudentDBContext studentDB)
    {
        this.studentDB=studentDB;
    }
    
    public IActionResult Index()
    {
        var stdData=studentDB.Students.ToList();
        return View(stdData);
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
