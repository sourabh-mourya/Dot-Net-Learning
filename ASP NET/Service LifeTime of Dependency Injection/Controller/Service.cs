using Microsoft.AspNetCore.Mvc;
using DependencyInjectionServices.Services;

namespace DependencyInjectionServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LifetimeController : ControllerBase
    {
        private readonly IService1 _a;
        private readonly IService1 _b;

        public LifetimeController(IService1 a, IService1 b)
        {
            _a = a;
            _b = b;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                First = _a.ServiceId(),
                Second = _b.ServiceId(),
                SameInstance = _a.ServiceId() == _b.ServiceId()
            });
        }
    }
}