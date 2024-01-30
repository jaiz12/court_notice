using API.Services;
using BAL.Services.BirthdayWish;
using BAL.Services.Common;
using BAL.Services.EmployeeOperations.EmployeeBirthday;
using BAL.Services.Master.BoardsService;
using DTO.Models;
using DTO.Models.Auth;
using DTO.Models.BirthdayWishesDTO;
using DTO.Models.Employee;
using DTO.Models.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.EmployeeOperationsController
{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmployeeBirthdayController : ControllerBase
    {
        private readonly IEmployeeBirthdayService _IEmployeeBirthdayService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        private readonly IHubContext<BirthdayWishService> _birthdayHubContext;
        private readonly ICommonService _commonService; 
        public EmployeeBirthdayController
        (
            IEmployeeBirthdayService IEmployeeBirthdayService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAuthoriseRoles authoriseRoles,
            IHubContext<BirthdayWishService> birthdayHubContext,
            ICommonService commonService
        )
        {
            _IEmployeeBirthdayService = IEmployeeBirthdayService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;
            _birthdayHubContext = birthdayHubContext;
            _commonService = commonService;
        }

        [HttpGet("GetBirthday/{userId}/{companyName}")]
        public async Task<IActionResult> GetBirthday(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var birthday = await _IEmployeeBirthdayService.GetBirthday();
            return Ok(new { result = birthday });
        }

        [HttpGet("GetBirthdayComment/{employee_id}/{userId}/{companyName}")]
        public async Task<IActionResult> GetBirthdayComment(string userId, string companyName, string employee_id)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var birthday = await _IEmployeeBirthdayService.GetBirthdayComment(employee_id);
            return Ok(new { result = birthday });
        }


        [HttpGet("GetBirthdayCommentByBirthdayPersonIdAndTimestamp/{userId}/{companyName}")]
        public async Task<IActionResult> GetBirthdayCommentByBirthdayPersonIdAndTimestamp(string employee_id, DateTime? timestamp, string userId, string companyName)
        { 
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(timestamp == null)
            {
                var birthday = await _IEmployeeBirthdayService.GetBirthdayComment(employee_id);
                return Ok(new { result = birthday });
            }
            else
            {
                List<EmployeeBirthday_DTO> birthdayWishes = await _IEmployeeBirthdayService.GetBirthdayCommentByBirthdayPersonIdAndTimestamp(employee_id, timestamp);
                return Ok(new { result = birthdayWishes });
            }

        }

        [HttpPost("PostBirthdayComment/{userId}/{companyName}")]
        public async Task<IActionResult> Post(EmployeeBirthday_DTO data, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Employee', 'Manager'" ,_roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _IEmployeeBirthdayService.Post(data);
            return Ok(new { result = comment });
        }

        [HttpDelete("DeleteBirthdayComment/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> Delete(long id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _IEmployeeBirthdayService.Delete(id);
            return Ok(new { result = comment });
        }


        /// <summary>
        /// Get Real Time Birthday Chat List of Birthday Persons - Pranai Giri - 20 JAN 2024
        /// </summary>
        [HttpGet("GetBirthdayListForRealTimeChat/{userId}/{companyName}")]
        public async Task<IActionResult> GetBirthdayListForRealTimeChat(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var birthday = await _IEmployeeBirthdayService.GetBirthdayListForRealTimeChat();
            return Ok(birthday);
        }

        /// <summary>
        /// Send Real Time Chat - 24 JAN 2024
        /// </summary>
        [HttpPost("SendOrUpdateBirthdayWish/{userId}/{companyName}")]
        public async Task<IActionResult> SendBirthdayWish([FromBody] BirthdayWishesDTO birthdayWish, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                DataResponse res = null;
                if (birthdayWish.birthday_comment_id > 0)
                {
                     res = await _commonService.PostOrUpdateAsync("emp_update_birthday_comment", birthdayWish, true);
                }
                else
                {
                     res = await _commonService.PostOrUpdateAsync("emp_post_birthday_comment", birthdayWish, false);
                }

                //await BroadcastBirthdayWishes(birthdayWish.employee_id);


                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error:" + ex.Message); // Handle the error gracefully
            }
        }


        /// <summary>
        /// Delete Real Time Chat - 24 JAN 2024
        /// </summary>
        [HttpPost("DeleteBirthdayWish/{userId}/{companyName}")]
        public async Task<IActionResult> DeleteBirthdayWish([FromBody] BirthdayWishesDTO birthdayWish, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                DataResponse res = await Task.Run(() => _commonService.DeleteById("emp_delete_birthday_comment", "prm_birthday_comment_id", birthdayWish.birthday_comment_id));
                //var connectionId = HttpContext.Connection.Id;
                //await BroadcastBirthdayWishes(birthdayWish.employee_id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error:" + ex.Message); // Handle the error gracefully
            }
        }


        /// <summary>
        /// Get Real Time Chat - 24 JAN 2024
        /// </summary>
        [HttpGet("GetWishesListByBirthdayPersonEmployeeId/{employee_id}/{userId}/{companyName}")]
        public async Task<IActionResult> GetWishesListByBirthdayPersonEmployeeId(string employee_id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Employee', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                List<EmployeeBirthday_DTO> birthdayWishes = await _commonService.GetListByIdAsync<EmployeeBirthday_DTO>("emp_get_birthday_comment", "prm_employee_id", employee_id);
                return Ok(birthdayWishes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error:" + ex.Message); // Handle the error gracefully
            }
        }


        private async Task BroadcastBirthdayWishes(string employeeId)
        {
            List<EmployeeBirthday_DTO> birthdayWishes = await _commonService.GetListByIdAsync<EmployeeBirthday_DTO>("emp_get_birthday_comment", "prm_employee_id", employeeId);
            await _birthdayHubContext.Clients.All.SendAsync($"ReceiveBirthdayWishes_{employeeId}", birthdayWishes);
        }

    }
}
