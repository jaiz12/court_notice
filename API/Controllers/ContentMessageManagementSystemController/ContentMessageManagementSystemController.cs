using API.Services;
using BAL.Services.ContentManagementSystem.ContentMessageManagementSystem;
using DTO.Models.Auth;
using DTO.Models.ContentManagementSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Threading.Tasks;

namespace API.Controllers.ContentMessageManagementSystemController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    // =============================================
    // -- Author:		Mukesh Shah
    // -- Create date:  05-Jan-2024
    // =============================================
    public class ContentMessageManagementSystemController : ControllerBase
    {
        private readonly IContentMessageManagementSystemService _contentMessageManagementSystemService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;

        public ContentMessageManagementSystemController(IContentMessageManagementSystemService contentMessageManagementSystemService, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAuthoriseRoles authoriseRoles)
        {
            _contentMessageManagementSystemService = contentMessageManagementSystemService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }
        [HttpPost("PostCmsMessage/{userId}/{companyName}")]
        public async Task<IActionResult> PostCmsMessage(ContentMessageManagementSystem_DTO message, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Company Head','Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _contentMessageManagementSystemService.PostCmsMessage(message);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("PutCmsMessage/{userId}/{companyName}")]
        public async Task<IActionResult> PutCmsMessage(ContentMessageManagementSystem_DTO message, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Company Head','Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _contentMessageManagementSystemService.PutCmsMessage(message);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeleteCmsMessage/{userId}/{companyName}/{message_id}")]
        public async Task<IActionResult> DeleteCmsMessage(long message_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Company Head','Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _contentMessageManagementSystemService.DeleteCmsMessage(message_id);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetCmsMessage/{userId}/{companyName}")]
        public async Task<IActionResult> GetCmsMessage(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Company Head','Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _contentMessageManagementSystemService.GetCmsMessage();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetCmsMessageForLogin/{userId}/{companyName}")]
        public async Task<IActionResult> GetCmsMessageForLogin(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Company Head','Super Admin', 'Employee'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _contentMessageManagementSystemService.GetCmsMessageForLogin();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
