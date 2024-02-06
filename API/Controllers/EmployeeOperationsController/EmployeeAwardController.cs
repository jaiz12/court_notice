using API.Services;
using BAL.Services.EmployeeOperations.EmployeeAwardService;
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
    public class EmployeeAwardController : ControllerBase
    {
        private readonly IEmployeeAwardService _employeeAwardService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;

        public EmployeeAwardController(IEmployeeAwardService employeeAwardService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _employeeAwardService = employeeAwardService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }

        [HttpGet("GetEmployeeBasicServiceForAward/{username}/{userId}/{companyName}")]
        public async Task<IActionResult> GetEmployeeBasicServiceForAward(string username, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeeAwardService.GetEmployeeBasicServiceForAward(username);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("PostEmployeeAward/{userId}/{companyName}")]
        public async Task<IActionResult> PostEmployeeAward(EmployeeAward_DTO employeeAward, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeeAwardService.PostEmployeeAward(employeeAward);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("GetEmployeeAward/{userId}/{companyName}")]
        public async Task<IActionResult> GetEmployeeAward(EmpAwardFilter_DTO filters, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Leave Admin','Company Head', 'Employee', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeeAwardService.GetEmployeeAward(filters);
                return Ok(result);

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("PutEmployeeAward/{userId}/{companyName}")]
        public async Task<IActionResult> PutEmployeeAward(EmployeeAwardEdit_DTO employeeAward, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeeAwardService.PutEmployeeAward(employeeAward);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeleteEmployeeAward/{userId}/{companyName}/{employee_award_id}")]
        public async Task<IActionResult> DeleteEmployeeAward(string userId, string companyName, long employee_award_id)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeeAwardService.DeleteEmployeeAward(employee_award_id);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("GetEmployeeDetailsForAward/{userId}/{companyName}/")]
        public async Task<IActionResult> GetEmployeeDetailsForAward(EmployeeAwardFilter_DTO filters, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeeAwardService.GetEmployeeDetailsForAward(filters);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
