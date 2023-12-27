using API.Services;
using BAL.Services.Common;
using DTO.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.EmployeeControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePromotion : Controller
    {
        private readonly ICommonService _commonService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        private readonly string userRoles = "'Admin','Super Admin'";
        public EmployeePromotion(
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
    }
}
