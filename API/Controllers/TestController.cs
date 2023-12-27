using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {


        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Test Successfully");
        }
    }
}
