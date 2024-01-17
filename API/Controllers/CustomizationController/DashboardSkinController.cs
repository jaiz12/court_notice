using API.Services;
using BAL.Services.Customization.DashboardSkinService;
using DTO.Models.Auth;
using DTO.Models.Customization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.CustomizationController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardSkinController : ControllerBase
    {
        private readonly IDashboardSkinService _dashboardSkinService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public DashboardSkinController(IDashboardSkinService dashboardSkinService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _dashboardSkinService = dashboardSkinService;
            _authoriseRoles = authoriseRoles;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpPost("PostOrUpdateDashboardSkin/{userId}/{companyName}")]
        public async Task<IActionResult> PostOrUpdateDashboardSkin(List<DashboardSkin_DTO> dashboardSkin, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _dashboardSkinService.PostOrUpdateDashboardSkin(dashboardSkin);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetDashboardSkin/{userId}/{companyName}")]
        public async Task<IActionResult> GetDashboardSkin(string userId, string companyName)
        {
            try
            {
                var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Leave Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager);
                if (!userCompanyRoleValidate)
                {
                    return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
                }
                try
                {
                    var result = await _dashboardSkinService.GetDashboardSkin();
                    return Ok(result);
                }
                catch
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
