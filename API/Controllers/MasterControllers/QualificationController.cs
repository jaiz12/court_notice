using API.Services;
using BAL.Services.Master.MasterServices.Qualification;
using DTO.Models.Auth;
using DTO.Models.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QualificationController : ControllerBase
    {
        private IQualificationService _qualificationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public QualificationController(IQualificationService qualificationService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _qualificationService = qualificationService;
            _authoriseRoles = authoriseRoles;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("GetQualification/{userId}/{companyName}")]
        public async Task<IActionResult> Get(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Employee', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            var result = _qualificationService.Get();
            return Ok(new { result = result });
        }

        [HttpPost("PostQualification/{userId}/{companyName}")]
        public async Task<IActionResult> Post(QualificationDTO item, string userId, string companyName)
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
            var result = _qualificationService.Post(item);
            return Ok(new { result = result });
        }

        [HttpPut("UpdateQualification/{userId}/{companyName}")]
        public async Task<IActionResult> Put(QualificationDTO item, string userId, string companyName)
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
            var result = _qualificationService.Update(item);
            return Ok(new { result = result });
        }

        [HttpDelete("DeleteQualification/{userId}/{companyName}/{id}")]
        //[Route("Delete/{id}")]
        public async Task<IActionResult> Delete(long id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            var result = await _qualificationService.Delete(id);
            return Ok(new { result = result });
        }

    }
}
