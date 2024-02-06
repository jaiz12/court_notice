using API.Services;
using BAL.Services.Common;
using DTO.Models.Auth;
using DTO.Models.Common;
using DTO.Models.EmployeeOperation;
using DTO.Models.EmployeeOperation.Transfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.EmployeeOperationsController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeTransferController : ControllerBase
    {
        private readonly ICommonService _commonService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public EmployeeTransferController(
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

        [HttpGet("GetEmployeeTransferDetailsByEmployeeId/{userId}/{companyName}")]
        public async Task<IActionResult> GetEmployeeTransferDetailsByEmployeeId(string employee_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _commonService.GetByIdAsync<EmployeeTransferDTO>("emp_get_employee_transfer_operation_details", "employee_id", employee_id);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("GetEmployeeForTransferDetails/{userId}/{companyName}")]
        public async Task<IActionResult> SearchEmployeeForTransferDetails(PaginationEntityDTO pagination, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _commonService.GetListByIdAsync<EmployeeTransferDTO>("emp_get_employee_transfer_operation_details", null, null, pagination);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("GetEmployeeTransferHistory/{userId}/{companyName}")]
        public async Task<IActionResult> GetEmployeeTransferHistory(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                return Ok(await _commonService.GetListByIdAsync<EmployeeTransferHistoryDTO>("emp_get_employee_transfer_history", null, null));
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("DeleteEmployeeTransferHistoryByEmployeeTransferLogId/{userId}/{companyName}")]
        public async Task<IActionResult> DeleteEmployeeTransferHistoryByEmployeeTransferLogId(long employee_transfer_log_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            return Ok(_commonService.DeleteById("emp_delete_employee_transfer_history_by_employee_transfer_log_id", "employee_transfer_log_id", employee_transfer_log_id));
        }

        [HttpPost("GetEmployeeForTransferDetailsWithSearchFilter/{userId}/{companyName}")]
        public async Task<IActionResult> SearchEmployeeForTransferDetailsWithSearchFilter(SearchFilterDTO searchFilter, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _commonService.GetListByIdUsingSearchFilterAsync<EmployeeTransferDTO>("emp_get_employee_transfer_operation_details_with_search_filter", null, null, searchFilter);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("GetTransferHistoryWithSearchFilter/{userId}/{companyName}")]
        public async Task<IActionResult> GetTransferHistoryWithSearchFilter(SearchFilterDTO searchFilter, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _commonService.GetListByIdUsingSearchFilterAsync<EmployeeTransferHistoryDTO>("emp_get_employee_transfer_history_with_search_filter", null, null, searchFilter);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost("PostEmployeeForTransferDetails/{userId}/{companyName}")]
        public async Task<IActionResult> PostEmployeeForTransferDetails([FromForm] EmployeePostTransferDTO transferDetails, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                return Ok(await _commonService.PostOrUpdateAsync("emp_post_employee_transfer_operation_details", transferDetails, false)); ;
            }
            catch
            {
                return BadRequest();
            }
        }




    }
}
