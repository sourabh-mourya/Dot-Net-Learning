using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;


namespace OrderServices.Controllers
{


    [Route("api/[Controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly HttpClient _httpClient;//issi ki help se me api ko call krunga..

        public OrderController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();//abhi maine yaha pr intailze kiya hi then isko used krke me direct call kr lunga..

        }

        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            //ab mujhe product ko get krna to uski api ko call krunga me direct yaha pr..
            //GetStringAsync ab iske ander ke url pass krunga 
            //wo url  hoga jiss hit krke apne apne products ko get kra tha..

            // var products=await _httpClient.GetFromJsonAsync<List>("http://localhost:5235/api/Product");

            //AGAR APNE KO json ME PROPER DATA CHIYE TO YE KRNA PADEGA APNE KO 
            //JsonElement: kisi bhi shape ka JSON le leta hai, class ki zarurat nahi
            var products = await _httpClient.GetFromJsonAsync<JsonElement>("http://localhost:5235/api/Product");


            return Ok(new
            {
                Message = "Orders Data Fetch Successfully",
                productData = products
            });


        }

    }

}