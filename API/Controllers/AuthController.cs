using API.Services;
using BAL.Services.Auth;
using Common.DataContext;
using DTO.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


// =============================================
// -- Author:		Jaideep Roy
// -- Create date: 23-Feb-2024
// =============================================

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAspNetRoles _aspNetRoles;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager,
            IAspNetRoles aspNetRoles, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _authService = authService;
            _userManager = userManager;
            _aspNetRoles = aspNetRoles;
            _signInManager = signInManager;
            _configuration = configuration;
        }



        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<object> Login(AspNetUsers_Login_DTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string errors = null;
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            var userlist = _userManager.FindByNameAsync(model.Email).Result;
            if (userlist == null)
            {
                return new Exception(errors = "User Can't Login With This Email and Password. Please Contact Your Admin.");
            }
            if (result.Succeeded)
            {

                var userroles = _userManager.GetRolesAsync(userlist).Result;
                string roles = "";
                for (int i = 0; i < userroles.Count; i++)
                {
                    var comma = userroles.Count - 1 != i ? "," : "";
                    roles += userroles[i] + comma;
                }
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Email);
                var usersid = appUser.Id;
                var tokenString = GenerateJwtToken(model.Email, roles, appUser);

                return Ok(new
                {
                    token = tokenString,
                    role = userroles,
                    userid = usersid
                });
            }
            return new Exception(errors = "Login Failed");



        }

        private object GenerateJwtToken(string email, string roles, IdentityUser user)
        {
            try
            {
                string[] userRoles = roles.Split(",");
                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
                for (int i = 0; i < userRoles.Length; i++)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRoles[i]));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

                var token = new JwtSecurityToken(
                    _configuration["JwtIssuer"],
                    _configuration["JwtIssuer"],
                    claims,
                    expires: expires,
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("CreateUser")]
        [Authorize(Roles = "Land Registration,Marriage Registration, Inheriantance Registration, Case Hearing, Others")]
        public async Task<IActionResult> CreateUser(AspNetUsers_Register_DTO register_DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userEmailExist = await _userManager.FindByEmailAsync(register_DTO.Email);
            DataTable roles = await _authService.GetRoles();
            var rolesSync = await _aspNetRoles.AspNetRole(roles);
            if (!rolesSync)
            {
                return Ok(new { message = "Roles Not Sync please try Again", messageDescription = "", messageType = "error" });
            }
            if (userEmailExist != null)
            {
                return Ok(new { message = "Already Employee Email Exist", messageDescription = "", messageType = "error" });
            }

            return Ok(await _authService.CreateUser(register_DTO));
        }

        [HttpPost("EditUser")]
        [Authorize(Roles = "Land Registration,Marriage Registration, Inheriantance Registration, Case Hearing, Others")]
        public async Task<IActionResult> EditUser(AspNetUsers_Edit_DTO register_DTO)
        {
            DataTable roles = await _authService.GetRoles();
            var rolesSync = await _aspNetRoles.AspNetRole(roles);
            if (!rolesSync)
            {
                return Ok(new { message = "Roles Not Sync please try Again", messageDescription = "", messageType = "error" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _authService.EditUser(register_DTO));
        }

        [HttpDelete("DeleteUser/{user_id}")]
        [Authorize(Roles = "Land Registration,Marriage Registration, Inheriantance Registration, Case Hearing, Others")]
        public async Task<IActionResult> Delete(string user_id)
        {

            try
            {
                var result = await _authService.DeleteUser(user_id);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("GetUsers")]
        [Authorize(Roles = "Land Registration,Marriage Registration, Inheriantance Registration, Case Hearing, Others")]
        public async Task<object> GetUsers()
        {
            return Ok(await _authService.GetUsers());

        }

        [HttpGet("GetRoles")]
        [Authorize(Roles = "Land Registration,Marriage Registration, Inheriantance Registration, Case Hearing, Others")]
        public async Task<object> GetRoles()
        {
            return Ok(await _authService.GetRoles());

        }

        
    }


}
