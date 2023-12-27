using API.Services;
using BAL.Services.Common;
using BAL.Services.Reports.EmployeeDetailsReportService;
using DTO.Models.Auth;
using DTO.Models.Employee;
using DTO.Models.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.Reports
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDetailsReport : Controller
    {
        private readonly IEmployeeDetailsReportService _employeeDetailsReportService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public EmployeeDetailsReport(
             IEmployeeDetailsReportService employeeDetailsReportService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAuthoriseRoles authoriseRoles
            )
        {
            _employeeDetailsReportService = employeeDetailsReportService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;

        }

        [HttpPost("GetEmployeeDetailsReport/{userId}/{companyName}")]
        [Authorize]
        public async Task<IActionResult> GetEmployeeDetailsReport(FilterReports data, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeeDetailsReportService.GetEmployeeDetailsReport(data);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("GetEmployeeServiceHistoryReport/{userId}/{companyName}")]
        [Authorize]
        public async Task<IActionResult> GetEmployeeServiceHistoryReport(FilterReports data, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeeDetailsReportService.GetEmployeeServiceHistoryReport(data);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("GetEmployeeLeaveHistoryReport/{userId}/{companyName}")]
        [Authorize]
        public async Task<IActionResult> GetEmployeeLeaveHistoryReport(EmpLeaveHistoryFilter data, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeeDetailsReportService.GetEmployeeLeaveHistoryReport(data);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
