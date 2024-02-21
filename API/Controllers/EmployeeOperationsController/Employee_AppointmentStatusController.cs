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
    public class Employee_AppointmentStatusController : Controller
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

        [HttpPost("GetEmployee_AppointmentStatus/{userId}/{companyName}")]
        public async Task<IActionResult> GetEmployee_AppointmentStatus(Employee_AppointmentStatus_DTO model,string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeePromotionService.GetEmployee_AppointmentStatus(model);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        } 
        
        [HttpPut("UpdateEmployee_AppointmentStatus/{userId}/{companyName}")]
        public async Task<IActionResult> UpdateEmployee_AppointmentStatus(Employee_AppointmentStatus_DTO_Edit model,string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeePromotionService.UpdateEmployee_AppointmentStatus(model);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }

        
        [HttpPost("GetEmployee_AppointmentStatusHistory/{userId}/{companyName}")]
        public async Task<IActionResult> GetEmployee_AppointmentStatusHistory(Employee_AppointmentStatus_DTO model,string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeePromotionService.GetEmployee_AppointmentStatusHistory(model);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPut("UpdateEmployee_AppointmentStatus_Revert/{userId}/{companyName}")]
        public async Task<IActionResult> UpdateEmployee_AppointmentStatus_Revert(Employee_AppointmentStatus_DTO_Revert model, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeePromotionService.UpdateEmployee_AppointmentStatus_Revert(model);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
