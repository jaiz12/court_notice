using API.Services;
using BAL.Services.EmployeeOperations.Employee_AppointmentStatusService;
using BAL.Services.EmployeeOperations.EmployeePromotionService;
using DTO.Models.Auth;
using DTO.Models.EmployeeOperation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.EmployeeOperationsController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Employee_AppointmentStatusController : ControllerBase
    {
        private readonly IEmployee_AppointmentStatusService _employeePromotionService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;

        public Employee_AppointmentStatusController(IEmployee_AppointmentStatusService employeePromotionService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _employeePromotionService = employeePromotionService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }

        [HttpPost("GetEmployeeBasicServiceForPromotion/{userId}/{companyName}")]

        public async Task<IActionResult> GetEmployeeBasicServiceForPromotion(EmployeePromotionOptionValues_DTO optionValues,string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeePromotionService.GetEmployee_AppointmentStatus(optionValues);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("PromoteEmployeeDesignation/{userId}/{companyName}")]
        public async Task<IActionResult> PromoteEmployeeDesignation(PromotedEmployeeService_DTO promote, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeePromotionService.PromoteEmployeeDesignation(promote);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("GetEmployeePromotionHistory/{userId}/{companyName}")]
        public async Task<IActionResult> GetEmployeePromotionHistory(EmployeePromotionOptionValues_DTO optionValues, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeePromotionService.GetEmployeePromotionHistory(optionValues);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("PutPromotedEmployeeDetails/{userId}/{companyName}")]
        public async Task<IActionResult> PutPromotedEmployeeDetails(EditPromotedEmployee_DTO dataModel, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeePromotionService.PutPromotedEmployeeDetails(dataModel);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeletePromotedEmployeeHistoryDetails/{userId}/{companyName}/{employee_service_id}")]
        public async Task<IActionResult> DeletePromotedEmployeeHistoryDetails(long employee_service_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeePromotionService.DeletePromotedEmployeeHistoryDetails(employee_service_id);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
