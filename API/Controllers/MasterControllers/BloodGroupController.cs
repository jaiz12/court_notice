using API.Services;
using BAL.Services.Master.BloodGroupService;
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
    // =============================================
    // -- Author:		Mukesh Shah
    // -- Create date: 01-Nov-2023
    // =============================================
    public class BloodGroupController : ControllerBase
    {
        private readonly IBloodGroupService _bloodGroupService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public BloodGroupController(IBloodGroupService bloodGroupService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _bloodGroupService = bloodGroupService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }
        [HttpPost("PostBloodGroup/{userId}/{companyName}")]
        //[Authorize(Roles ="Super Admin,Admin")]
        public async Task<IActionResult> PostBloodGroup(BloodGroup_DTO bloodGroupList, string userId, string companyName)
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
            try
            {
                var result = await _bloodGroupService.PostBloodGroup(bloodGroupList);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("PutBloodGroup/{userId}/{companyName}")]
        public async Task<IActionResult> PutBloodGroup(BloodGroup_DTO bloodGroupList, string userId, string companyName)
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
            try
            {
                var result = await _bloodGroupService.PutBloodGroup(bloodGroupList);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetBloodGroup/{userId}/{companyName}")]
        public async Task<IActionResult> GetBloodGroup(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Employee', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _bloodGroupService.GetBloodGroup();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeleteBloodGroup/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> DeleteBloodGroup(string userId, string companyName, long id)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _bloodGroupService.DeleteBloodGroup(id);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
