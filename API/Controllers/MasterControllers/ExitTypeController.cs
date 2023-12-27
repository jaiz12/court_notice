using API.Services;
using BAL.Services.Master.ExitTypeService;
using DTO.Models.Auth;
using DTO.Models.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExitTypeController : Controller
    {
        private readonly IExitTypeService _exitTypeService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public ExitTypeController(IExitTypeService exitTypeService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _exitTypeService = exitTypeService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }

        [HttpGet("GetExitType/{userId}/{companyName}")]
        public async Task<IActionResult> GetExitType(string userId, string companyName)
        {
            try
            {
                var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Employee', 'Company Head', 'Manager'", _roleManager, _userManager);
                if (!userCompanyRoleValidate)
                {
                    return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _exitTypeService.GetExitType();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("AddExitType/{userId}/{companyName}")]
        public async Task<IActionResult> AddExitTypeAsync(ExitType_DTO model, string userId, string companyName)
        {
            try
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
                var result = _exitTypeService.AddExitType(model);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("EditExitType/{userId}/{companyName}")]
        public async Task<IActionResult> EditExitTypeAsync(ExitType_DTO model, string userId, string companyName)
        {
            try
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
                var result = _exitTypeService.EditExitType(model);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteExitType/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> DeleteExitType(string userId, string companyName, long id)
        {
            try
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
                var result = await _exitTypeService.DeleteExitType(id);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
