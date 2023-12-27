using API.Services;
using BAL.Services.Master.BranchOfficeService;
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
    // -- Create date: 09-Nov-2023
    // =============================================
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchOfficeService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public BranchController(IBranchService branchOfficeService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _branchOfficeService = branchOfficeService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }
        [HttpPost("PostBranch/{userId}/{companyName}")]
        public async Task<IActionResult> PostBranch(Branch_DTO branchOffice, string userId, string companyName)
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
                var result = await _branchOfficeService.PostBranch(branchOffice);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("PutBranch/{userId}/{companyName}")]
        public async Task<IActionResult> PutBranch(Branch_DTO branchOffice, string userId, string companyName)
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
                var result = await _branchOfficeService.PutBranch(branchOffice);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeleteBranch/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> DeleteBranch(string userId, string companyName, long id)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _branchOfficeService.DeleteBranch(id);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetBranch/{userId}/{companyName}")]
        public async Task<IActionResult> GetBranch(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Employee', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _branchOfficeService.GetBranch();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
