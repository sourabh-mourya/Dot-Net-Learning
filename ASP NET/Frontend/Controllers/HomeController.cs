using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using MyASPCoreApp.Models;

namespace Frontend.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        List<Student> students = new List<Student>
        {
          new Student {Id=1,Name="Hello world",Gender = "Male"},
          new Student {Id=1,Name="Hello world",Gender = "Male"},
          new Student {Id=1,Name="Hello world",Gender = "Male"},
          new Student {Id=1,Name="Hello world",Gender = "Male"}
        };
        return View(students);
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
