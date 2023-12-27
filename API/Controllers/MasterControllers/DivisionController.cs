using API.Services;
using BAL.Services.Master.DivisionService;
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
    // -- Create date: 02-Nov-2023
    // =============================================
    public class DivisionController : ControllerBase
    {
        private readonly IDivisionService _divisionService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public DivisionController(IDivisionService divisionService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _divisionService = divisionService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }
        [HttpPost("PostDivision/{userId}/{companyName}")]
        public async Task<IActionResult> PostDivision(Division_DTO division, string userId, string companyName)
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
                var result = await _divisionService.PostDivision(division);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("PutDivision/{userId}/{companyName}")]
        public async Task<IActionResult> PutDivision(Division_DTO division, string userId, string companyName)
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
                var result = await _divisionService.PutDivision(division);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetDivision/{userId}/{companyName}")]
        public async Task<IActionResult> GetDivision(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Employee', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _divisionService.GetDivision();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeleteDivision/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> DeleteDivision(string userId, string companyName, long id)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _divisionService.DeleteDivision(id);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
