using API.Services;
using BAL.Services.Common;
using BAL.Services.Master.CompanyService;
using DTO.Models.Auth;
using DTO.Models.Employee;
using DTO.Models.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
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
        ICommonService _commonService;

        public CompanyController(ICompanyService companyService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IAuthoriseRoles authoriseRoles, ICommonService commonService)
        {
            _companyService = companyService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
            _commonService = commonService;
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



        /// <summary>
        /// Author : Pranai Giri
        /// Description : Company Logo Upload
        /// Date : 29 Dec 2023
        /// </summary>
        [HttpPost]
        [Route("UpdateCompanyLogo/{userId}/{companyName}")]
        public async Task<IActionResult> PostOrUpdateCompanyLogo([FromForm] CompanyLogoDTO data, string userId, string companyName)
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

            if (Request.Form.Files.Count > 0)
            {
                foreach (var file in Request.Form.Files)
                {
                    var ext = Path.GetExtension(file.FileName) == "" ? ".webp" : file.ContentType.Contains("image") ?
                        ".webp" : Path.GetExtension(file.FileName);
                    var folderName = Path.Combine("assets", "company", "company_logo");
                    var filepath = Path.Combine(folderName, data.company_name.Replace(" ", "") + ext);

                    // Ensure the directory exists before saving the file
                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }

                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    data.company_logo_url = filepath;
                }
            }


            return Ok(await _commonService.PostOrUpdateAsync("emp_update_company_logo_url", data, false));
        }


    }
}
