using API.Services;
using BAL.Services.ContentManagementSystem.ContentManagementSystem;
using BAL.Services.SignalR;
using DTO.Models.Auth;
using DTO.Models.ContentManagementSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace API.Controllers.ContentManagementSystemController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    // =============================================
    // -- Author:		Mukesh Shah
    // -- Create date:  07-Dec-2023
    // =============================================
    public class ContentManagementSystemController : ControllerBase
    {
        private readonly IContentManagementSystemService _contentManagementSystemService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        private readonly IHubContext<NotificationAlert> _notificationAlertHubContext;
        public ContentManagementSystemController(IContentManagementSystemService contentManagementSystemService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAuthoriseRoles authoriseRoles,
            IHubContext<NotificationAlert> notificationAlertHubContext)
        {
            _contentManagementSystemService = contentManagementSystemService;
            _authoriseRoles = authoriseRoles;
            _userManager = userManager;
            _roleManager = roleManager;
            _notificationAlertHubContext = notificationAlertHubContext;
        }
        [HttpPost("PostCMSDetails/{userId}/{companyName}")]
        public async Task<IActionResult> PostCMSDetails(ContentManagementSystem_DTO cmsDetails, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Company Head','Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _contentManagementSystemService.PostCMSDetails(cmsDetails);
                await _notificationAlertHubContext.Clients.All.SendAsync("NewNoticeAlert");
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("PutCMSDetails/{userId}/{companyName}")]
        public async Task<IActionResult> PutCMSDetails(ContentManagementSystem_DTO cmsDetails, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Company Head','Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _contentManagementSystemService.PutCMSDetails(cmsDetails);
                await _notificationAlertHubContext.Clients.All.SendAsync("NewNoticeAlert");
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("DeleteCMSDetails/{userId}/{companyName}/{cms_id}")]
        public async Task<IActionResult> DeleteCMSDetails(long cms_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Company Head','Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _contentManagementSystemService.DeleteCMSDetails(cms_id);
                await _notificationAlertHubContext.Clients.All.SendAsync("NewNoticeAlert");
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetCMSDetails/{userId}/{companyName}")]
        public async Task<IActionResult> GetCMSDetails(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Company Head','Super Admin','Leave Admin','Employee','Manager','Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _contentManagementSystemService.GetCMSDetails();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetCMSDetailsOnLoad/{userId}/{companyName}")]
        public async Task<IActionResult> GetCMSDetailsOnLoad(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Leave Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _contentManagementSystemService.GetCMSDetailsOnLoad();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetCmsNoticeCount/{userId}/{companyName}")]
        public async Task<IActionResult> GetCmsNoticeCount(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Leave Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _contentManagementSystemService.GetCmsNoticeCount();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
