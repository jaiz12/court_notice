using API.Services;
using BAL.Services.Common;
using BAL.Services.EmployeeOperations.EmployeePrintCV;
using DTO.Models.Auth;
using DTO.Models.Common;
using DTO.Models.EmployeeOperation.PrintCV;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.EmployeeOperationsController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeePrintCVController : ControllerBase
    {
        private readonly ICommonService _commonService;
        private readonly IEmployeePrintCVService _employeePrintCVService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public EmployeePrintCVController(
            ICommonService commonService,
            IEmployeePrintCVService employeePrintCVService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAuthoriseRoles authoriseRoles
            )
        {
            _commonService = commonService;
            _employeePrintCVService = employeePrintCVService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
        }


        [HttpPost("GetEmployeeListForPrintCV/{userId}/{companyName}")]
        public async Task<IActionResult> GetEmployeeListForPrintCV(PaginationEntityDTO pagination, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _employeePrintCVService.GetEmployeeListForPrintCV(pagination);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("GetResumePDF/{userId}/{companyName}")]
        public async Task<FileContentResult> GetResumePDF(EmployeeDetailsForPrintCVDTO employee, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                throw new Exception("Unauthorize User.You are not authorize to use the module. Please contact with your admin for the permission");
            }
            try
            {
                var result = await _employeePrintCVService.GenerateEmployeeResumePdf(employee);
                return File(result, "application/pdf");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("GetResumesZIP/{userId}/{companyName}")]
        public async Task<FileContentResult> GetResumesZIP(List<EmployeeDetailsForPrintCVDTO> employeeList, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                throw new Exception("Unauthorize User.You are not authorize to use the module. Please contact with your admin for the permission");
            }
            try
            {
                var result = await _employeePrintCVService.GenerateEmployeesResumeZip(employeeList);
                return File(result, "application/zip");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
