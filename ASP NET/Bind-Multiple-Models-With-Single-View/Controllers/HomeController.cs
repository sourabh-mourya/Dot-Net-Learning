using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bind_Multiple_Models_With_Single_View.Models;

namespace Bind_Multiple_Models_With_Single_View.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        List<Student> students = new List<Student>
        {
        new Student { Id = 1, Name = "Rahul Sharma", Gender = "Male", Standard = 10 },
        new Student { Id = 2, Name = "Priya Verma", Gender = "Female", Standard = 12 },
        new Student { Id = 3, Name = "Amit Kumar", Gender = "Male", Standard = 9 },
        new Student { Id = 4, Name = "Sneha Patel", Gender = "Female", Standard = 11 },
        new Student { Id = 5, Name = "Rohit Singh", Gender = "Male", Standard = 8 },
        new Student { Id = 6, Name = "Anjali Gupta", Gender = "Female", Standard = 10 },
        new Student { Id = 7, Name = "Karan Joshi", Gender = "Male", Standard = 12 }
    };

        List<Teacher> teachers = new List<Teacher>
        {
    new Teacher { Id = 1, Name = "Amit Sharma", Email = "amit@gmail.com", Gender = "Male", Subject = "Mathematics", Experience = 8, Salary = 55000 },
    new Teacher { Id = 2, Name = "Priya Verma", Email = "priya@gmail.com", Gender = "Female", Subject = "Science", Experience = 5, Salary = 45000 },
    new Teacher { Id = 3, Name = "Rahul Patel", Email = "rahul@gmail.com", Gender = "Male", Subject = "English", Experience = 10, Salary = 65000 },
    new Teacher { Id = 4, Name = "Sneha Gupta", Email = "sneha@gmail.com", Gender = "Female", Subject = "Computer Science", Experience = 6, Salary = 50000 },
    new Teacher { Id = 5, Name = "Rohit Singh", Email = "rohit@gmail.com", Gender = "Male", Subject = "Physics", Experience = 12, Salary = 75000 },
    new Teacher { Id = 6, Name = "Anjali Joshi", Email = "anjali@gmail.com", Gender = "Female", Subject = "Chemistry", Experience = 4, Salary = 40000 },
    new Teacher { Id = 7, Name = "Karan Mehta", Email = "karan@gmail.com", Gender = "Male", Subject = "Biology", Experience = 7, Salary = 52000 }
};

        SchoolViewModel svm = new SchoolViewModel()
        {
            myStudents = students, //ye dono jo properity banayi thi wo ek list student ki to apne yaha pr pass kr di uss list ko 
            myTeachers = teachers
        };
        return View(svm);
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
