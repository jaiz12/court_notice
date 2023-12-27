using API.Services;
using BAL.Services.EmployeeOperations.EmployeeBirthday;
using BAL.Services.Master.BoardsService;
using DTO.Models.Auth;
using DTO.Models.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.EmployeeOperationsController
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmployeeBirthdayController : ControllerBase
    {
        private readonly IEmployeeBirthdayService _IEmployeeBirthdayService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public EmployeeBirthdayController
        (
            IEmployeeBirthdayService IEmployeeBirthdayService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAuthoriseRoles authoriseRoles
        )
        {
            _IEmployeeBirthdayService = IEmployeeBirthdayService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }

        [HttpGet("GetBirthday/{userId}/{companyName}")]
        public async Task<IActionResult> GetBirthday(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var birthday = await _IEmployeeBirthdayService.GetBirthday();
            return Ok(new { result = birthday });
        }

        
    }
}
