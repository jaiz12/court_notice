using API.Services;
using BAL.Services.Master.MasterServices.AppointmentStatus;
using DTO.Models.Auth;
using DTO.Models.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.MasterController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentStatusController : ControllerBase
    {
        private IAppointmentStatusService _appointmentStatusService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthoriseRoles _authoriseRoles;
        public AppointmentStatusController
        (
            IAppointmentStatusService appointmentStatusService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAuthoriseRoles authoriseRoles
        )
        {
            _appointmentStatusService = appointmentStatusService;
            _userManager = userManager;
            _roleManager = roleManager;
            _authoriseRoles = authoriseRoles;

        }

        [HttpGet]
        [Route("GetAppointmentStatus/{userId}/{companyName}")]
        public async Task<IActionResult> Get(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Employee', 'Company Head', 'Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            try
            {
                var result = await _appointmentStatusService.Get();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("PostAppointmentStatus/{userId}/{companyName}")]
        public async Task<IActionResult> Post(AppointmentStatusDTO item, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //return Ok(_appointmentStatusService.Post(item));
            try
            {
                var result = _appointmentStatusService.Post(item);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("UpdateAppointmentStatus/{userId}/{companyName}")]
        public async Task<IActionResult> Put(AppointmentStatusDTO item, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //return Ok(_appointmentStatusService.Update(item));
            try
            {
                var result = _appointmentStatusService.Update(item);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("DeleteAppointmentStatus/{userId}/{companyName}/{id}")]
        public async Task<IActionResult> Delete(long id, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin', 'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            //return Ok(await _appointmentStatusService.Delete(id));
            try
            {
                var result = _appointmentStatusService.Delete(id);
                return Ok(new { result = result });
            }
            catch
            {
                return BadRequest();
            }

            
        }

    }
}
