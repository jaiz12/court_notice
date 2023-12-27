using API.Services;
using BAL.Services.EmployeeOperations.EmployeeExtensionService;
using DTO.Models.Auth;
using DTO.Models.EmployeeOperation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.EmployeeOperationsController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeExtension_TerminationController : ControllerBase
    {
        private readonly IEmployeeExtension_TerminationService _employeeExtensionService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;

        public EmployeeExtension_TerminationController(IEmployeeExtension_TerminationService employeeExtensionService, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _employeeExtensionService = employeeExtensionService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }

        [HttpPost("GetEmployeeServiceForExtensionTermination/{userId}/{companyName}")]

        public async Task<IActionResult> GetEmployeeServiceForExtensionTermination(EmployeeExtension_Termination_DTO optionValues, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeeExtensionService.GetEmployeeServiceForExtensionTermination(optionValues);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("PostEmployeeExtensionTermination/{userId}/{companyName}")]
        public async Task<IActionResult> PostEmployeeExtensionTermination(Employee_ExtensionTermination_DTO employee, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeeExtensionService.PostEmployeeExtensionTermination(employee);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost("GetEmployeeService_ExtensionTermination_Log/{userId}/{companyName}")]

        public async Task<IActionResult> GetEmployeeService_ExtensionTermination_Log(EmployeeExtension_Termination_DTO optionValues, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeeExtensionService.GetEmployeeService_ExtensionTermination_Log(optionValues);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
