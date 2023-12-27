using API.Services;
using BAL.Services.Master.DesignationService;
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
    public class DesignationController : Controller
    {
        private readonly IDesignationService _designationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public DesignationController(IDesignationService designationService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _designationService = designationService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }

        [HttpGet("GetDesignation/{userId}/{companyName}")]
        public async Task<IActionResult> GetDesignation(string userId, string companyName)
        {
            try
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
                var result = await _designationService.GetDesignations();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("AddDesignation/{userId}/{companyName}")]
        public async Task<IActionResult> AddDesignationAsync(Designation_DTO model, string userId, string companyName)
        {
            try
            {
                var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin'", _roleManager, _userManager);
                if (!userCompanyRoleValidate)
                {
                    return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = _designationService.AddDesignation(model);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("EditDesignation/{userId}/{companyName}")]
        public async Task<IActionResult> EditDesignationAsync(Designation_DTO model, string userId, string companyName)
        {
            try
            {
                var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin'", _roleManager, _userManager);
                if (!userCompanyRoleValidate)
                {
                    return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = _designationService.EditDesignation(model);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteDesignation/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> DeleteDesignation(string userId, string companyName, long id)
        {
            try
            {
                var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin'", _roleManager, _userManager);
                if (!userCompanyRoleValidate)
                {
                    return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _designationService.DeleteDesignation(id);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
