using API.Services;
using BAL.Services.Common;
using DTO.Models;
using DTO.Models.Auth;
using DTO.Models.Employee;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.EmployeeControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePersonalDetailsController : ControllerBase
    {
        ICommonService _commonService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        private readonly string userRoles = "'Admin','Leave Admin','Employee', 'Company Head', 'Manager'";
        public EmployeePersonalDetailsController
        (
            ICommonService commonService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAuthoriseRoles authoriseRoles
        )
        {
            _commonService = commonService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }

        [HttpGet]
        [Route("Get/{userId}/{companyName}")]
        public async Task<IActionResult> GetById(string employee_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            return Ok(_commonService.GetById<EmployeePersonalDetailsDTO>("emp_get_employee_personal_details_by_employeeid", "employee_id", employee_id));
        }

        [HttpPost]
        [Route("Post/{userId}/{companyName}")]
        public async Task<IActionResult> Post(EmployeePersonalDetailsDTO item, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_commonService.PostOrUpdate("emp_post_employee_personal_details", item, false));
        }

        [HttpPut]
        [Route("Update/{userId}/{companyName}")]
        public async Task<IActionResult> Update(EmployeePersonalDetailsDTO item, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_commonService.PostOrUpdate("emp_update_employee_personal_details", item, true));
        }

        [HttpDelete]
        [Route("Delete/{userId}/{companyName}")]
        public async Task<IActionResult> DeleteById(string employee_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            return Ok(_commonService.DeleteById("emp_delete_employee_personal_details_by_employee_id", "employee_id", employee_id));
        }

        [HttpPost]
        [Route("UpdateProfilePhoto/{userId}/{companyName}")]
        public async Task<IActionResult> PostOrUpdateProfilePhoto([FromForm] EmployeeProfilePhotoDTO data, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
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
                    var filename = file.FileName.Split(".")[0];
                    var folderName = Path.Combine("assets", "employee", "profile_photos", data.employee_id ?? userId);
                    var filepath = Path.Combine(folderName, Guid.NewGuid() + "_" + filename.Replace(" ", "") + ext);

                    // Ensure the directory exists before saving the file
                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }

                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    data.filepath = filepath;
                }
            }


            return Ok(await _commonService.PostOrUpdateAsync("emp_update_employee_profile_photo", data, false));
        }
        
        
        [HttpGet]
        [Route("GetEmployeeProfilePhoto/{userId}/{companyName}")]
        public async Task<IActionResult> GetProfilePhotoByEmployeeId(string employee_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            return Ok(await _commonService.GetByIdAsync<EmployeeProfilePhotoDTO>("emp_get_employee_profile_photo_by_employeeid", "employee_id", employee_id));
        }

        [HttpGet]
        [Route("GetEmployeeEisProfileCompletionDetails/{userId}/{companyName}")]
        public async Task<IActionResult> GetEmployeeEisProfileCompletionDetails(string employee_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }

            return Ok(await _commonService.GetDatasetByIdAsync("emp_get_employee_eis_profile_completion_details", "employee_id", employee_id));
        }


    }
}
