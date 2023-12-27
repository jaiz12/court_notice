using API.Services;
using BAL.Services.RolesCompanyPermission;
using DTO.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
// =============================================
// -- Author:		Jaideep Roy
// -- Create date: 17-Nov-2023
// =============================================
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesCompanyPermissionController : ControllerBase
    {
        private readonly IRolesCompanyPermissionService _roleCompanyPermissionService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public RolesCompanyPermissionController(IRolesCompanyPermissionService roleCompanyPermissionService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _roleCompanyPermissionService = roleCompanyPermissionService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }

        [HttpPost("PostRoleCompanyPermission/{userId}/{companyName}")]
        //[Authorize(Roles ="Super Admin,Admin")]
        public async Task<IActionResult> PostRoleCompanyPermission(RoleCompanyPermission_DTO modal, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _roleCompanyPermissionService.PostRoleCompanyPermission(modal);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut("UpdateRoleCompanyPermission/{userId}/{companyName}")]
        public async Task<IActionResult> UpdateRoleCompanyPermission(RoleCompanyPermission_DTO modal, string userId, string companyName)
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
            var result = await _roleCompanyPermissionService.UpdateRoleCompanyPermission(modal);
            return Ok(new { result = result });
        }


        [HttpGet("GetRoleCompanyPermission/{userId}/{companyName}")]
        public async Task<IActionResult> GetRoleCompanyPermission(string userId, string companyName)
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
            var result = await _roleCompanyPermissionService.GetRoleCompanyPermission();
            return Ok(new { result = result });
        }

        [HttpDelete("DeleteRoleCompanyPermission/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> DeleteRoleCompanyPermission(string userId, string companyName,long id)
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
            var result = await _roleCompanyPermissionService.DeleteRoleCompanyPermission(id);
            return Ok(new { result = result });
        }




    }
}
