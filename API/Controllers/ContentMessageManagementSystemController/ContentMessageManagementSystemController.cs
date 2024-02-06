using API.Services;
using BAL.Services.ContentManagementSystem.ContentMessageManagementSystem;
using BAL.Services.SignalR;
using DTO.Models.Auth;
using DTO.Models.ContentManagementSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<NotificationAlert> _notificationAlertHubContext;

        public ContentMessageManagementSystemController(IContentMessageManagementSystemService contentMessageManagementSystemService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAuthoriseRoles authoriseRoles,
            IHubContext<NotificationAlert> notificationAlertHubContext)
        {
            _contentMessageManagementSystemService = contentMessageManagementSystemService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
            _notificationAlertHubContext = notificationAlertHubContext;
        }
        [HttpPost("PostCmsMessage/{userId}/{companyName}")]
        public async Task<IActionResult> PostCmsMessage(ContentMessageManagementSystem_DTO message, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Leave Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager); if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _contentMessageManagementSystemService.PostCmsMessage(message);
                await _notificationAlertHubContext.Clients.All.SendAsync("NewMessageAlert");
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
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Leave Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager); if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _contentMessageManagementSystemService.PutCmsMessage(message);
                await _notificationAlertHubContext.Clients.All.SendAsync("NewMessageAlert");
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
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Leave Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager); if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _contentMessageManagementSystemService.DeleteCmsMessage(message_id);
                await _notificationAlertHubContext.Clients.All.SendAsync("NewMessageAlert");
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
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Leave Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager); if (!userCompanyRoleValidate)
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
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Leave Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager); if (!userCompanyRoleValidate)
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

        [HttpGet("GetCmsMessageCount/{userId}/{companyName}")]
        public async Task<IActionResult> GetCmsMessageCount(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Leave Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager); if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _contentMessageManagementSystemService.GetCmsMessageCount();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
