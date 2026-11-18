using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductServices.Models;

namespace ProductServices.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProduct()
        {
            //yaha pr abhi apne databse ko include nhi kr simple plain rkh rhe hi 
            var products = new List<Product>()
            {
                new Product { Id = 1, Name = "Laptop", Price = 50000 },
                new Product { Id = 2, Name = "Smartphone", Price = 25000 },
                new Product { Id = 3, Name = "Headphones", Price = 2000 },
                new Product { Id = 4, Name = "Keyboard", Price = 1500 },
                new Product { Id = 5, Name = "Mouse", Price = 800 },
                new Product { Id = 6, Name = "Monitor", Price = 12000 },
                new Product { Id = 7, Name = "Smart Watch", Price = 5000 },
                new Product { Id = 8, Name = "Tablet", Price = 18000 }
            };

            return Ok(products);
        }
    }
}