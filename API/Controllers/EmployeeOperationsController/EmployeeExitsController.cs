using API.Services;
using BAL.Services.EmployeeOperations.EmployeeExitsService;
using DTO.Models.Auth;
using DTO.Models.EmployeeOperation.Exits;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.EmployeeOperationsController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeExitsController : ControllerBase
    {
        private readonly IEmployeeExitsService _employeeExitsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;

        public EmployeeExitsController(IEmployeeExitsService employeeExitsService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _employeeExitsService = employeeExitsService;
            _userManager = userManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }

        [HttpPost("GetEmployeeBasicDetailsForExits/{userId}/{companyName}")]
        public async Task<IActionResult> GetEmployeeBasicDetailsForExits(EmployeeExitsOptionValue_DTO _optionValue, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = _employeeExitsService.GetEmployeeBasicDetailsForExits(_optionValue);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("PutEmployeeExits/{userId}/{companyName}")]
        public async Task<IActionResult> PutEmployeeExits(EmployeeExits_DTO _exit, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeeExitsService.PutEmployeeExits(_exit);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
