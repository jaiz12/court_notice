using API.Services;
using BAL.Services.Master.DistrictService;
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

    /// Author : Sandeep Chauhan
    /// Date : 07-11-2023
    public class DistrictController : Controller
    {
        private readonly IDistrictService _districtService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public DistrictController(IDistrictService districtService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles)
        {
            _districtService = districtService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }

        [HttpGet("GetDistrict/{userId}/{companyName}")]
        public async Task<IActionResult> GetDistrict(string userId, string companyName)
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
                var result = await _districtService.GetDistrict();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("AddDistrict/{userId}/{companyName}")]
        public async Task<IActionResult> AddDistrict(District_DTO model, string userId, string companyName)
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
                var result = await _districtService.AddDistrict(model);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("EditDistrict/{userId}/{companyName}")]
        public async Task<IActionResult> EditDistrict(District_DTO model, string userId, string companyName)
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
                var result = await _districtService.EditDistrict(model);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteDistrict/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> DeleteDistrict(string userId, string companyName, long id)
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
                var result = await _districtService.DeleteDistrict(id);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
