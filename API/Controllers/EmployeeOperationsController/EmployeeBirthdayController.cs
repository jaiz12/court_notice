using API.Services;
using BAL.Services.EmployeeOperations.EmployeeBirthday;
using BAL.Services.Master.BoardsService;
using DTO.Models.Auth;
using DTO.Models.Employee;
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

        [HttpGet("GetBirthdayComment/{employee_id}/{userId}/{companyName}")]
        public async Task<IActionResult> GetBirthdayComment(string userId, string companyName, string employee_id)
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
            var birthday = await _IEmployeeBirthdayService.GetBirthdayComment(employee_id);
            return Ok(new { result = birthday });
        }

        [HttpPost("PostBirthdayComment/{userId}/{companyName}")]
        public async Task<IActionResult> Post(EmployeeBirthday_DTO data, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Employee', 'Manager'" ,_roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _IEmployeeBirthdayService.Post(data);
            return Ok(new { result = comment });
        }

        [HttpDelete("DeleteBirthdayComment/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> Delete(long id, string userId, string companyName)
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
            var comment = await _IEmployeeBirthdayService.Delete(id);
            return Ok(new { result = comment });
        }


        /// <summary>
        /// Controller For Real Time Birthday Chat - Pranai Giri - 20 JAN 2024
        /// </summary>
        [HttpGet("GetBirthdayListForRealTimeChat/{userId}/{companyName}")]
        public async Task<IActionResult> GetBirthdayListForRealTimeChat(string userId, string companyName)
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
            var birthday = await _IEmployeeBirthdayService.GetBirthdayListForRealTimeChat();
            return Ok(birthday);
        }

    }
}
