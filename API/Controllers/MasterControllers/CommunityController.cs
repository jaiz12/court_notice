using API.Services;
using BAL.Services.Master.MasterServices.Community;
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
    [Authorize]
    [ApiController]
    [Authorize]
    public class CommunityController : ControllerBase
    {

        private ICommunityService _communityService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public CommunityController(ICommunityService communityService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _communityService = communityService;
            _authoriseRoles = authoriseRoles;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("GetCommunity/{userId}/{companyName}")]
        public async Task<IActionResult> Get(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Employee', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            return Ok(_communityService.Get());
        }

        [HttpPost("PostCommunity/{userId}/{companyName}")]
        public async Task<IActionResult> Post(CommunityDTO item, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _communityService.Post(item);
            return Ok(new { result = result });
        }

        [HttpPut("UpdateCommunity/{userId}/{companyName}")]
        public async Task<IActionResult> Put(CommunityDTO item, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _communityService.Update(item);
            return Ok(new { result = result });
        }

        [HttpDelete("DeleteCommunity/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> Delete(long id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            var result = await _communityService.Delete(id);
            return Ok(new { result = result });
        }

    }
}
