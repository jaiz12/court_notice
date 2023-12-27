using API.Services;
using BAL.Services.Master.MasterServices.ResidentialStatus;
using DTO.Models.Auth;
using DTO.Models.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResidentialStatusController : ControllerBase
    {
        private IResidentialStatusService _residentialStatusService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public ResidentialStatusController
            (
                IResidentialStatusService residentialStatusService,
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager,
                IAuthoriseRoles authoriseRoles
            )
        {
            _residentialStatusService = residentialStatusService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }

        [HttpGet]
        [Route("GetResidentialStatus/{userId}/{companyName}")]
        public async Task<IActionResult> Get(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Employee', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            return Ok(_residentialStatusService.Get());
        }

        [HttpPost]
        [Route("GetResidentialStatus/{userId}/{companyName}")]
        public async Task<IActionResult> Post(ResidentialStatusDTO item, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_residentialStatusService.Post(item));
        }

        [HttpPut]
        [Route("GetResidentialStatus/{userId}/{companyName}")]
        public async Task<IActionResult> Put(ResidentialStatusDTO item, string userId, string companyName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_residentialStatusService.Update(item));
        }

        [HttpDelete]
        [Route("GetResidentialStatus/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> Delete(long id, string userId, string companyName)
        {
            return Ok(await _residentialStatusService.Delete(id));
        }
    }
}
