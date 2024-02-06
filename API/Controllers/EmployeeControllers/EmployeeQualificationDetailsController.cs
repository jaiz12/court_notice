using API.Services;
using BAL.Services.Common;
using DTO.Models;
using DTO.Models.Auth;
using DTO.Models.Employee;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.EmployeeControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeQualificationDetailsController : ControllerBase
    {
        private readonly ICommonService _commonService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        private readonly string userRoles = "'Admin', 'Company Head', 'Employee', 'Manager'";
        public EmployeeQualificationDetailsController(
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
        public async Task<IActionResult> GetByEmployeeId(string employee_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            return Ok(_commonService.GetListById<EmployeeQualificationDetailsDTO>("emp_get_employee_qualification_details_by_employee_id", "employee_id", employee_id));
        }

        [HttpPost]
        [Route("Post/{userId}/{companyName}")]
        public async Task<IActionResult> Post([FromForm] EmployeeQualificationDetailsDTO item, string userId, string companyName)
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
                    // Define the allowed file extensions
                    var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png", ".webp" };

                    // Get the file extension
                    var ext = Path.GetExtension(file.FileName).ToLower();

                    // Check if the file extension is allowed
                    if (allowedExtensions.Contains(ext))
                    {
                        var filename = Path.GetFileNameWithoutExtension(file.FileName);
                        var folderName = Path.Combine("assets", "employee", "qualification", "attachments", item.employee_id ?? userId);
                        var filePath = Path.Combine(folderName, Guid.NewGuid() + "_" + filename.Replace(" ", "") + ext);

                        // Ensure the directory exists before saving the file
                        if (!Directory.Exists(folderName))
                        {
                            Directory.CreateDirectory(folderName);
                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        item.attachment_url = filePath;
                    }
                    else
                    {
                        // Handle invalid file type
                        // You might want to return an error or take appropriate action
                    }
                }
            }

            DataResponse dr = await _commonService.PostOrUpdateAsync("emp_post_employee_qualification_details", item, false);

            if (dr.IsSucceeded == true)
            {
                dr.Message = "Qualification Details Added Successfully!";
            }
            return Ok(dr);
        }

        [HttpPut]
        [Route("Update/{userId}/{companyName}")]
        public async Task<IActionResult> Update([FromForm] EmployeeQualificationDetailsDTO item, string userId, string companyName)
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
                    // Define the allowed file extensions
                    var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png", ".webp" };

                    // Get the file extension
                    var ext = Path.GetExtension(file.FileName).ToLower();

                    // Check if the file extension is allowed
                    if (allowedExtensions.Contains(ext))
                    {
                        var filename = Path.GetFileNameWithoutExtension(file.FileName);
                        var folderName = Path.Combine("assets", "employee", "qualification", "attachments", item.employee_id ?? userId);
                        var filePath = Path.Combine(folderName, Guid.NewGuid() + "_" + filename.Replace(" ", "") + ext);

                        // Ensure the directory exists before saving the file
                        if (!Directory.Exists(folderName))
                        {
                            Directory.CreateDirectory(folderName);
                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        item.attachment_url = filePath;
                    }
                    else
                    {
                        // Handle invalid file type
                        // You might want to return an error or take appropriate action
                    }
                }
            }

            DataResponse dr = await _commonService.PostOrUpdateAsync("emp_update_employee_qualification_details", item, true);

            if (dr.IsSucceeded == true)
            {
                dr.Message = "Qualification Details Updated Successfully!";
            }
            return Ok(dr);
        }

        [HttpDelete]
        [Route("Delete/{userId}/{companyName}")]
        public async Task<IActionResult> DeleteById(long employee_qualification_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, userRoles, _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            return Ok(_commonService.DeleteById("emp_delete_employee_qualification_details_by_employee_qualification_id", "employee_qualification_id", employee_qualification_id));
        }
    }
}
