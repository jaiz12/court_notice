
using API.Services;
using BAL.Services.Master.ModeOfTrainingService;
using DTO.Models.Auth;
using DTO.Models.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.MasterController
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ModeOfTrainingController : ControllerBase
    {
        private readonly IModeOfTraining _IModeOfTraining;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public ModeOfTrainingController(IModeOfTraining IModeOfTraining, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _IModeOfTraining = IModeOfTraining;
            _authoriseRoles = authoriseRoles;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("PostModeOfTraining/{userId}/{companyName}")]
        public async Task<IActionResult> Post(Mode_of_training_DTO data, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mode_of_training = await _IModeOfTraining.Post(data);
            return Ok(new { result = mode_of_training });
        }

        [HttpGet("GetModeOfTraining/{userId}/{companyName}")]
        public async Task<IActionResult> GetMaritalStatus(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Employee', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mode_of_training = await _IModeOfTraining.GetModeOfTraining();
            return Ok(new { result = mode_of_training });
        }

        [HttpPut("UpdateModeOfTraining/{userId}/{companyName}")]
        public async Task<IActionResult> Update(Mode_of_training_DTO id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mode_of_training = await _IModeOfTraining.Update(id);
            return Ok(new { result = mode_of_training });
        }

        [HttpDelete("DeleteModeOfTraining/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> Delete(long id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mode_of_training = await _IModeOfTraining.Delete(id);
            return Ok(new { result = mode_of_training });
        }
    }
}
