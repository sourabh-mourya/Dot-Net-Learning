using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsAPIController : ControllerBase
    {
        public List<string> fruits = new List<string>()
          {
                "Apple" ,
                "Banana",
                "Mango",
                "Cherry",
                "Grapes"
          };


        [HttpGet]
        //jb ye wala url type hoga api/FruitsAPI --> and and wo Get request rhegi tb ye wala action method function call ho jyega...
        public List<string> GetFruits()
        {
            return fruits;
        }

        [HttpGet("{id }")]
        public string GetFruitsByIndex(int id)
        {
            return fruits.ElementAt(id);
        }
    }
}