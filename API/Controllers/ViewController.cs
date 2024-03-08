using BAL.Services.Marriage;
using BAL.Services.View;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewController : Controller
    {
        private readonly IViewService _viewService;
        public ViewController(IViewService viewService)
        {
            _viewService = viewService;
        }
        [HttpGet("GetCountOfView")]

        public async Task<IActionResult> GetCountOfView()
        {
            return Ok(await _viewService.GetCountOfView());

        }

        [HttpGet("GetMarriageRegisteredDetails")]

        public async Task<IActionResult> GetMarriageRegisteredDetails()
        {
            return Ok(await _viewService.GetMarriageRegisteredDetails());

        }

        [HttpGet("GetLandRegisteredDetails")]
        public async Task<object> GetLandRegisteredDetails()
        {

            try
            {
                var result = await _viewService.GetLandRegisteredDetails();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
