using Microsoft.AspNetCore.Mvc;
using LoginFormASPCoreMVC.Models;
using LoginFormASPCoreMVC.Data;

namespace LoginFormASPCoreMVC.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDBContext applicationDB;

    public HomeController(ApplicationDBContext applicationDB)
    {
        this.applicationDB = applicationDB;
    }

    public IActionResult Index()
    {
        return View();
    }

    //GET : Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    //Post:Register
    [HttpPost]
    public async Task<IActionResult> Register(Student std)
    {
        if (ModelState.IsValid)
        {
            await applicationDB.Students.AddAsync(std);
            await applicationDB.SaveChangesAsync();
            ViewBag.RegisterMessage = "Registration is successfully......................";
            return RedirectToAction("Login");
        }
        return View(std);
    }

    // GET: Login
    [HttpGet]
    public IActionResult Login()
    {
        // Already logged in?
        if (HttpContext.Session.GetString("UserSession") != null)
        {
            return RedirectToAction("Dashboard");
        }

        return View();
    }

    // POST: Login
    [HttpPost]
    public IActionResult Login(Student std)
    {
        var myUserStudents = applicationDB.Students
            .FirstOrDefault(x =>
                x.Email == std.Email &&
                x.Password == std.Password);

        if (myUserStudents != null)
        {
            HttpContext.Session.SetString(
                "UserSession",
                myUserStudents.Email
            );

            return RedirectToAction("Dashboard");
        }

        ViewBag.Message = "Login Failed...........";
        return View();
    }

    // GET: Dashboard
    [HttpGet]
    public IActionResult Dashboard()
    {
        var session = HttpContext.Session.GetString("UserSession");

        if (session == null)
        {
            return RedirectToAction("Login");
        }

        ViewBag.MySession = session;
        ViewBag.MySessionID = HttpContext.Session.Id;

        return View();
    }

    // GET: Logout
    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("UserSession");

        return RedirectToAction("Login");
    }
}