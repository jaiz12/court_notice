using API.Services;
using BAL.Services.Master.CasteService;
using DTO.Models.Auth;
using BAL.Services.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.DashboardController
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]

    /// Author : Sandeep Chauhan
    /// Date : 28-12-2023
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public DashboardController(IDashboardService dashboardService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _dashboardService = dashboardService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }

        [HttpGet("GetTotalEmployeeWithCompany/{userId}/{companyName}")]
        public async Task<IActionResult> GetTotalEmployeeWithCompany(string userId, string companyName)
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
                var result = await _dashboardService.GetTotalEmployeeWithCompany();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
