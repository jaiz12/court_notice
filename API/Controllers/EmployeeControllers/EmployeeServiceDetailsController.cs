using API.Services;
using BAL.Services.Common;
using DTO.Models.Auth;
using DTO.Models.Employee;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.EmployeeControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeServiceDetailsController : ControllerBase
    {
        private readonly ICommonService _commonService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        private readonly string userRoles = "'Admin','Employee', 'Company Head', 'Manager'";
        public EmployeeServiceDetailsController(
            ICommonService commonService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAuthoriseRoles authoriseRoles
            )
        {
            _commonService = commonService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }

        [HttpGet]
        [Route("Get/{userId}/{companyName}")]
        public async Task<IActionResult> GetListById(string employee_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            return Ok(_commonService.GetListById("emp_get_employee_service_details_by_employeeid", "employee_id", employee_id));
        }

        [HttpPost]
        [Route("Post/{userId}/{companyName}")]
        public async Task<IActionResult> Post(EmployeeServiceDetailsDTO item, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_commonService.PostOrUpdate("emp_post_employee_service_details", item, false));
        }

        [HttpPut]
        [Route("Update/{userId}/{companyName}")]
        public async Task<IActionResult> Update(EmployeeServiceDetailsDTO item, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_commonService.PostOrUpdate("emp_update_employee_service_details", item, true));
        }

        [HttpDelete]
        [Route("Delete/{userId}/{companyName}")]
        public async Task<IActionResult> DeleteById(string employee_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            return Ok(_commonService.DeleteById("emp_delete_employee_service_details_by_employee_id", "employee_id", employee_id));
        }
    }
}
