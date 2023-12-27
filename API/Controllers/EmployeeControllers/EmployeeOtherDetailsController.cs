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
    public class EmployeeOtherDetailsController : ControllerBase
    {
        ICommonService _commonService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        private readonly string userRoles = "'Admin','Employee', 'Company Head', 'Manager'";
        public EmployeeOtherDetailsController(
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
        public async Task<IActionResult> GetById(string employee_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please other with your admin for the permission" });
            }
            return Ok(_commonService.GetById<EmployeeOtherDetailsDTO>("emp_get_employee_other_details_by_employeeid", "employee_id", employee_id));
        }

        [HttpPost]
        [Route("Post/{userId}/{companyName}")]
        public async Task<IActionResult> Post(EmployeeOtherDetailsDTO item, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please other with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_commonService.PostOrUpdate("emp_post_employee_other_details", item, false));
        }

        [HttpPut]
        [Route("Update/{userId}/{companyName}")]
        public async Task<IActionResult> Update(EmployeeOtherDetailsDTO item, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please other with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_commonService.PostOrUpdate("emp_update_employee_other_details", item, true));
        }

        [HttpDelete]
        [Route("Delete/{userId}/{companyName}")]
        public async Task<IActionResult> DeleteById(string employee_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please other with your admin for the permission" });
            }
            return Ok(_commonService.DeleteById("emp_delete_employee_other_details_by_employee_id", "employee_id", employee_id));
        }
    }
}
