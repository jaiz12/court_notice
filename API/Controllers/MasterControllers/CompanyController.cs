using API.Services;
using BAL.Services.Master.CompanyService;
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
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;

        public CompanyController(ICompanyService companyService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _companyService = companyService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }

        [HttpGet("GetCompany/{userId}/{companyName}")]
        public async Task<IActionResult> GetCompany(string userId, string companyName)
        {

            // var userId = "string";
            // var companyName = "SIBIN Group";

            //var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin'", _roleManager, _userManager);
            // if (!userCompanyRoleValidate)
            // {
            //     return BadRequest("Unathorized User.");
            // }
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
                var result = await _companyService.GetCompany();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("AddCompany/{userId}/{companyName}")]
        public async Task<IActionResult> AddCompanyAsync(Company_DTO model, string userId, string companyName)
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
                var result = _companyService.AddCompany(model);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("EditCompany/{userId}/{companyName}")]
        public async Task<IActionResult> EditCompanyAsync(Company_DTO model, string userId, string companyName)
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
                var result = _companyService.EditCompany(model);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteCompany/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> DeleteCompany(string userId, string companyName, long id)
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
                var result = await _companyService.DeleteCompany(id);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
