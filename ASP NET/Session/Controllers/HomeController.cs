using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Session.Models;
using Microsoft.AspNetCore.Http;

namespace Session.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {

        HttpContext.Session.SetString("MyKey", "ProgramMentor");
        //ye ek session bn gya session privacy action chlege waha pr ek session mil jyega
        //usko key ki help se accessss kr skte hi ....
        return View();
    }

    public IActionResult About()
    {

        if (HttpContext.Session.GetString("MyKey") != null)
        {
            ViewBag.Data = HttpContext.Session.GetString("MyKey").ToString();
        }

        return View();
    }

    public IActionResult GetDetails()
    {

        if (HttpContext.Session.GetString("MyKey") != null)
        {
            ViewBag.Data = HttpContext.Session.GetString("MyKey").ToString();
            ViewBag.SessionId = HttpContext.Session.Id;
        }

        return View();
    }

    public IActionResult Logout()
    {

        if (HttpContext.Session.GetString("MyKey") != null)
        {
            HttpContext.Session.Remove("MyKey");
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
