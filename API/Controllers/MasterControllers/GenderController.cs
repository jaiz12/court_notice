
using API.Services;
using BAL.Services.Master.GenderService;
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
    // -- Create date: 07-Nov-2023
    // =============================================
    public class GenderController : ControllerBase
    {
        private readonly IGenderService _genderService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public GenderController(IGenderService genderService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _genderService = genderService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }
        [HttpPost("PostGender/{userId}/{companyName}")]
        public async Task<IActionResult> PostGender(Gender_DTO gender, string userId, string companyName)
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
                var result = await _genderService.PostGender(gender);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("PutGender/{userId}/{companyName}")]
        public async Task<IActionResult> PutGender(Gender_DTO gender, string userId, string companyName)
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
                var result = await _genderService.PutGender(gender);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetGender/{userId}/{companyName}")]
        public async Task<IActionResult> GetGender(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Employee', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _genderService.GetGender();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeleteGender/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> DeleteGender(string userId, string companyName, long id)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _genderService.DeleteGender(id);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
