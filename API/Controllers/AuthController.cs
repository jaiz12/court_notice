using API.Services;
using BAL.Auth.AuthService;
using BAL.Services.Common;
using Common.DataContext;
using DTO.Models.Auth;
using DTO.Models.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


// =============================================
// -- Author:		Jaideep Roy
// -- Create date: 09-Nov-2023
// =============================================

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;
        private readonly bIAuthService _bIAuthService;
        private readonly IAuthoriseRoles _authoriseRoles;
        ICommonService _commonService;

        public AuthController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration,
            SignInManager<ApplicationUser> signInManager, ApplicationDbContext db,
             IEmailSender emailSender, bIAuthService bIAuthService, IAuthoriseRoles authoriseRoles,
             ICommonService commonService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _db = db;
            _emailSender = emailSender;
            _bIAuthService = bIAuthService;
            _authoriseRoles = authoriseRoles;
            _commonService = commonService;
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<object> Login([FromBody] AspNetUsers_Login_DTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string errors = null;
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (result.Succeeded)
            {
                var userlist = _userManager.FindByNameAsync(model.UserName).Result;
                if (!userlist.UserStatus)
                {
                    return new Exception(errors = "User Can't Login With This UserName and Password. Please Contact Your Admin.");
                }
                var userroles = _userManager.GetRolesAsync(userlist).Result;
                DataTable userCompanyRoleMapping = await _bIAuthService.GetCompanyRolesMappingByUserId(userlist.Id);
                List<AspNetUserRoles_Register_DTO> aspNetUserRoles = new List<AspNetUserRoles_Register_DTO>();
                for (int i = 0; i < userCompanyRoleMapping.Rows.Count; i++)
                {
                    AspNetUserRoles_Register_DTO UserRoles = new AspNetUserRoles_Register_DTO();
                    UserRoles.CompanyName = userCompanyRoleMapping.Rows[i]["company_name"].ToString();
                    UserRoles.RoleName = userCompanyRoleMapping.Rows[i]["Name"].ToString();
                    aspNetUserRoles.Add(UserRoles);
                }
                string permission = null;
                string role = null;
                string company = null;
                if (aspNetUserRoles.Count == 1)
                {
                    DataTable roleCompanyMapping = await _bIAuthService.GetRoleCompanyPermissionByCompanyandRole(aspNetUserRoles[0].CompanyName, aspNetUserRoles[0].RoleName);
                    permission = roleCompanyMapping.Rows[0]["Permission"].ToString();
                    role = roleCompanyMapping.Rows[0]["Name"].ToString();
                    company = aspNetUserRoles[0].CompanyName;

                }
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.UserName);
                var usersid = appUser.Id;
                var tokenString = GenerateJwtToken(model.UserName, appUser);
                return Ok(new
                {
                    token = tokenString,
                    userid = usersid,
                    userroles = userroles,
                    aspNetUserRoles = aspNetUserRoles,
                    company = company,
                    permission = permission,
                    role = role,
                });
            }
            return new Exception(errors = "Login Failed");



        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost("RegisterEmployee/{userId}/{companyName}")]
        public async Task<object> RegisterEmployee([FromBody] AspNetUsers_Register_DTO registerUser, string userId, string companyName)
        {



            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }

            string errors = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string company = registerUser.Roles[0].CompanyName;
            string[] words = company.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Substring(0, 1);
            }
            company = string.Join("", words).ToLower();
            var Password = $"{registerUser.UserName}{company}{"@123"}";
            var userEmailExist = await _userManager.FindByEmailAsync(registerUser.Email);
            var userUserNameExist = await _userManager.FindByNameAsync(registerUser.UserName);
            if (userEmailExist != null)
            {
                return Ok(new { message = "Already Employee Email Exist", messageDescription = "", messageType = "error" });
            }
            if (userUserNameExist != null)
            {
                return Ok(new { message = "Already Employee Code Exist", messageDescription = "", messageType = "error" });
            }
            registerUser.Id = Guid.NewGuid().ToString();
            registerUser.CreatedOn = DateTime.Now;
            registerUser.UpdatedOn = DateTime.Now;
            var user = new ApplicationUser()
            {
                Id = registerUser.Id,
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                NormalizedUserName = registerUser.UserName.ToUpper(),
                NormalizedEmail = registerUser.Email.ToUpper(),
                UserStatus = true,
                CreatedOn = registerUser.CreatedOn,
                CreatedBy = registerUser.CreatedBy,
                UpdatedBy = registerUser.UpdatedBy,
                UpdatedOn = registerUser.UpdatedOn
            };
            var result = await _userManager.CreateAsync(user, Password);

            foreach (var role in registerUser.Roles)
            {
                role.UserId = user.Id;
                await _userManager.AddToRoleAsync(user, role.RoleName);
            }
            var userCompanyMapping = await _bIAuthService.UserCompanyRoleMapping(registerUser.Roles);
            var userProfile = new EmployeePersonalDetailsDTO()
            {
                employee_id = registerUser.UserName,
                first_name = registerUser.FirstName,
                middle_name = registerUser.MiddleName,
                last_name = registerUser.LastName,
                date_of_birth = registerUser.dob,
                gender_id = registerUser.gender,
                created_by = registerUser.CreatedBy,
                created_on = registerUser.CreatedOn,
                updated_by = registerUser.UpdatedBy,
                updated_on = registerUser.UpdatedOn,
            };

            var empProfile = _commonService.PostOrUpdateEmployeeProfile("emp_post_register_employee_personal_details", userProfile, false);
            var userService = new EmployeeServiceDetailsDTO()
            {
                employee_id = registerUser.UserName,
                company_id = registerUser.company,
                branchoffice_id = registerUser.branch,
                state_id = registerUser.state,
                district_id = registerUser.district,
                designation_id = registerUser.designation,
                division_id = registerUser.division,
                appointment_status_id = registerUser.appointmentStatus,
                exit_type_id = null,
                date_of_joining = registerUser.dateOfJoining,
                place_of_posting_id = registerUser.placeOfPosting,
                effective_from = registerUser.effective_from,
                effective_to = Convert.ToDateTime("9999-01-31 00:00:00.00"),
                is_active = true,
                created_by = registerUser.CreatedBy,
                created_on = registerUser.CreatedOn,
                updated_by = registerUser.UpdatedBy,
                updated_on = registerUser.UpdatedOn,
            };
            var empService = _commonService.PostOrUpdate("emp_post_employee_service_details", userService, false);
            var empWiseLeaveConfigure = await _bIAuthService.ConfigEmployeeWiseLeave(registerUser);


            if (result.Succeeded && userCompanyMapping != null && empProfile.IsSucceeded && empService.IsSucceeded)
            {
                await _emailSender.SendEmailAsync(registerUser.Roles[0].CompanyName, registerUser.Email, $"Welcome to {registerUser.Roles[0].CompanyName} - Your On boarding Information",
                   $"Dear {registerUser.FirstName} {registerUser.MiddleName} {registerUser.LastName},<br><br>Welcome to {registerUser.Roles[0].CompanyName}! We are delighted to have you on board as a valuable member of our team. We are excited about the contributions you will make.<br><br>To help you get started, here are some important details and information:<br>" +
                   $"<br><b>Employee Details:</b><br><b>Employee ID</b>: {registerUser.UserName}<br><b>Email ID</b>: {registerUser.Email}<br><br>" +
                   $"<b>HRMS Login Information:</b><br><b>Username</b>: {registerUser.UserName}<br><b>Password</b>: {Password}<br><br>" +
                   $"We look forward to working together and wish you a successful and fulfilling journey with us.");
                var tokenstring = GenerateJwtToken(registerUser.UserName, user);
                return Ok(new { message = "Employee Registered Successfully", messageDescription = "", messageType = "success" });
            }
            else
            {

                return Ok(new { message = "Error while trying to add a Employee", messageDescription = "", messageType = "error" });
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost("EditRegisterEmployee/{userId}/{companyName}")]
        public async Task<object> EditRegisterEmployee([FromBody] AspNetUsers_Register_DTO registerUser, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            string errors = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var user = await _userManager.FindByIdAsync(registerUser.Id);

            user.UserName = registerUser.UserName;
            user.Email = registerUser.Email;
            user.NormalizedUserName = registerUser.UserName.ToUpper();
            user.NormalizedEmail = registerUser.Email.ToUpper();
            user.UserStatus = true;
            user.UpdatedBy = registerUser.UpdatedBy;
            user.UpdatedOn = DateTime.Now;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var removeRole = await _userManager.RemoveFromRolesAsync((ApplicationUser)user, _userManager.GetRolesAsync(user).Result);
                if (removeRole != null)
                {
                    foreach (var role in registerUser.Roles)
                    {
                        await _userManager.AddToRoleAsync(user, role.RoleName);
                    }
                    await _bIAuthService.DeleteUserCompanyRoleMapping(user.Id);
                    await _bIAuthService.UserCompanyRoleMapping(registerUser.Roles);
                    var userProfile = new EmployeePersonalDetailsDTO()
                    {
                        employee_personal_id = registerUser.employee_personal_id,
                        employee_id = registerUser.UserName,
                        first_name = registerUser.FirstName,
                        middle_name = registerUser.MiddleName,
                        last_name = registerUser.LastName,
                        date_of_birth = registerUser.dob,
                        gender_id = registerUser.gender,
                        updated_by = registerUser.UpdatedBy,
                        updated_on = DateTime.Now,
                    };

                    _commonService.PostOrUpdateEmployeeProfile("emp_update_register_employee_personal_details", userProfile, false);
                    var userService = new EmployeeServiceDetailsDTO()
                    {
                        employee_service_id = registerUser.employee_service_id,
                        employee_id = registerUser.UserName,
                        company_id = registerUser.company,
                        branchoffice_id = registerUser.branch,
                        state_id = registerUser.state,
                        district_id = registerUser.district,
                        designation_id = registerUser.designation,
                        division_id = registerUser.division,
                        appointment_status_id = registerUser.appointmentStatus,
                        exit_type_id = null,
                        date_of_joining = registerUser.dateOfJoining,
                        place_of_posting_id = registerUser.placeOfPosting,
                        effective_from = registerUser.effective_from,
                        effective_to = Convert.ToDateTime("9999-01-31 00:00:00.00"),
                        is_active = true,
                        created_by = registerUser.CreatedBy,
                        created_on = registerUser.CreatedOn,
                        updated_by = registerUser.UpdatedBy,
                        updated_on = DateTime.Now,
                    };
                    _commonService.PostOrUpdate("emp_update_register_employee_service_details", userService, false);
                    await _emailSender.SendEmailAsync(registerUser.Roles[0].CompanyName, registerUser.Email, "Registered New Employee in HRMS",
                   "<b>Hello</b>,<br><br> <b> You have been registered in In-House HRMS </b><br><b>Your Credentials are below</b><br>" +
                   "<b>UserName</b>:" + registerUser.UserName);
                    var tokenstring = GenerateJwtToken(registerUser.UserName, user);

                }
                return Ok(new { message = "Employee Updated Successfully", messageDescription = "", messageType = "success" });
            }
            else
            {
                return Ok(new { message = "Error while trying to update a Employee", messageDescription = "", messageType = "error" });
            }
        }


        private object GenerateJwtToken(string email, IdentityUser user)
        {
            try
            {
                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

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

        [AllowAnonymous]
        [Authorize]
        [HttpPost("EmployeeActiveInActive/{userId}/{companyName}")]
        public async Task<object> EmployeeActiveInActive([FromBody] AspNetUsers_Register_DTO registerUser, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            string errors = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            ApplicationUser user = await _userManager.FindByNameAsync(registerUser.UserName);

            // Update it with the values from the view model
            user.UserStatus = registerUser.UserStatus == true ? false : true;
            user.UpdatedBy = registerUser.UpdatedBy;
            user.UpdatedOn = DateTime.Now;

            // Apply the changes if any to the db
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(new { message = "Changed Employee Status Successfully", messageDescription = "", messageType = "success" });
            }
            else
            {
                return Ok(new { message = "Error while trying to Change a Employee Status", messageDescription = "", messageType = "error" });
            }
        }

        [HttpGet("GetUser/{userId}/{companyName}")]
        [Authorize]
        public async Task<object> GetUser(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin', 'Company Head', 'Manager','Employee'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }


            List<ApplicationUser> users = await _userManager.Users.ToListAsync();
            List<AspNetUsers_Register_DTO> aspNetUsersList = new List<AspNetUsers_Register_DTO>();
            foreach (var user in users)
            {
                if (user.LockoutEnabled)
                {
                    AspNetUsers_Register_DTO aspNetUser = new AspNetUsers_Register_DTO();
                    DataTable PersonalAndServiceDetails = await _bIAuthService.GetPersonalAndServiceDetailsByEmployeeId(user.UserName);

                    if (PersonalAndServiceDetails.Rows[0]["company_name"].ToString() == companyName)
                    {
                        aspNetUser.employee_personal_id = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["employee_personal_id"]);
                        aspNetUser.employee_service_id = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["employee_service_id"]);
                        aspNetUser.FirstName = PersonalAndServiceDetails.Rows[0]["first_name"].ToString();
                        aspNetUser.MiddleName = PersonalAndServiceDetails.Rows[0]["middle_name"].ToString();
                        aspNetUser.LastName = PersonalAndServiceDetails.Rows[0]["last_name"].ToString();
                        aspNetUser.dob = Convert.ToDateTime(PersonalAndServiceDetails.Rows[0]["date_of_birth"]);
                        aspNetUser.gender = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["gender_id"]);
                        aspNetUser.company = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["company_id"]);
                        aspNetUser.companyName = PersonalAndServiceDetails.Rows[0]["company_name"].ToString();
                        aspNetUser.branch = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["branchoffice_id"]);
                        aspNetUser.branchName = PersonalAndServiceDetails.Rows[0]["branch_name"].ToString();
                        aspNetUser.state = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["state_id"]);
                        aspNetUser.stateName = PersonalAndServiceDetails.Rows[0]["state_name"].ToString();
                        aspNetUser.district = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["district_id"]);
                        aspNetUser.districtName = PersonalAndServiceDetails.Rows[0]["district_name"].ToString();
                        aspNetUser.designation = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["designation_id"]);
                        aspNetUser.designationName = PersonalAndServiceDetails.Rows[0]["designation_name"].ToString();
                        aspNetUser.division = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["division_id"]);
                        aspNetUser.divisionName = PersonalAndServiceDetails.Rows[0]["division_name"].ToString();
                        aspNetUser.appointmentStatus = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["appointment_status_id"]);
                        aspNetUser.appointmentStatusName = PersonalAndServiceDetails.Rows[0]["appointment_status_name"].ToString();
                        aspNetUser.dateOfJoining = Convert.ToDateTime(PersonalAndServiceDetails.Rows[0]["date_of_joining"]);
                        aspNetUser.placeOfPosting = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["place_of_posting_id"]);
                        aspNetUser.placeOfPostingName = PersonalAndServiceDetails.Rows[0]["place_of_posting_name"].ToString();
                        aspNetUser.effective_from = Convert.ToDateTime(PersonalAndServiceDetails.Rows[0]["effective_from"]);
                        aspNetUser.exit_type_id = PersonalAndServiceDetails.Rows[0]["exit_type_id"].ToString();
                        aspNetUser.is_active = Convert.ToBoolean(PersonalAndServiceDetails.Rows[0]["is_active"]);
                        aspNetUser.CreatedOn = Convert.ToDateTime(PersonalAndServiceDetails.Rows[0]["created_on"]);
                        aspNetUser.CreatedBy = PersonalAndServiceDetails.Rows[0]["created_by"].ToString();
                        aspNetUser.Id = user.Id;
                        aspNetUser.UserName = user.UserName;
                        aspNetUser.employeeCode = user.UserName;
                        aspNetUser.Email = user.Email;
                        aspNetUser.UserStatus = user.UserStatus;
                        DataTable userCompanyRoleMapping = await _bIAuthService.GetCompanyRolesMappingByUserId(user.Id);
                        List<AspNetUserRoles_Register_DTO> aspNetUserRoles = new List<AspNetUserRoles_Register_DTO>();
                        for (int i = 0; i < userCompanyRoleMapping.Rows.Count; i++)
                        {
                            AspNetUserRoles_Register_DTO UserRoles = new AspNetUserRoles_Register_DTO();
                            UserRoles.Id = Convert.ToUInt32(userCompanyRoleMapping.Rows[i]["id"]);
                            UserRoles.CompanyName = userCompanyRoleMapping.Rows[i]["company_name"].ToString();
                            UserRoles.RoleName = userCompanyRoleMapping.Rows[i]["Name"].ToString();
                            aspNetUserRoles.Add(UserRoles);
                        }
                        aspNetUser.Roles = aspNetUserRoles;
                        aspNetUsersList.Add(aspNetUser);
                    }
                }
            }
            return aspNetUsersList;

        }

        [HttpGet("GetActiveUser/{userId}/{companyName}")]
        [Authorize]
        public async Task<object> GetActiveUser(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }


            List<ApplicationUser> users = await _userManager.Users.ToListAsync();
            List<AspNetUsers_Register_DTO> aspNetUsersList = new List<AspNetUsers_Register_DTO>();
            foreach (var user in users)
            {
                if (user.UserStatus && user.LockoutEnabled)
                {
                    AspNetUsers_Register_DTO aspNetUser = new AspNetUsers_Register_DTO();
                    DataTable PersonalAndServiceDetails = await _bIAuthService.GetPersonalAndServiceDetailsByEmployeeId(user.UserName);
                    if (PersonalAndServiceDetails.Rows[0]["company_name"].ToString() == companyName)
                    {
                        aspNetUser.FirstName = PersonalAndServiceDetails.Rows[0]["first_name"].ToString();
                        aspNetUser.MiddleName = PersonalAndServiceDetails.Rows[0]["middle_name"].ToString();
                        aspNetUser.LastName = PersonalAndServiceDetails.Rows[0]["last_name"].ToString();
                        aspNetUser.dob = Convert.ToDateTime(PersonalAndServiceDetails.Rows[0]["date_of_birth"]);
                        aspNetUser.gender = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["gender_id"]);
                        aspNetUser.company = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["company_id"]);
                        aspNetUser.companyName = PersonalAndServiceDetails.Rows[0]["company_name"].ToString();
                        aspNetUser.branch = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["branchoffice_id"]);
                        aspNetUser.branchName = PersonalAndServiceDetails.Rows[0]["branch_name"].ToString();
                        aspNetUser.state = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["state_id"]);
                        aspNetUser.stateName = PersonalAndServiceDetails.Rows[0]["state_name"].ToString();
                        aspNetUser.district = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["district_id"]);
                        aspNetUser.districtName = PersonalAndServiceDetails.Rows[0]["district_name"].ToString();
                        aspNetUser.designation = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["designation_id"]);
                        aspNetUser.designationName = PersonalAndServiceDetails.Rows[0]["designation_name"].ToString();
                        aspNetUser.division = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["division_id"]);
                        aspNetUser.divisionName = PersonalAndServiceDetails.Rows[0]["division_name"].ToString();
                        aspNetUser.appointmentStatus = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["appointment_status_id"]);
                        aspNetUser.appointmentStatusName = PersonalAndServiceDetails.Rows[0]["appointment_status_name"].ToString();
                        aspNetUser.dateOfJoining = Convert.ToDateTime(PersonalAndServiceDetails.Rows[0]["date_of_joining"]);
                        aspNetUser.placeOfPosting = Convert.ToInt32(PersonalAndServiceDetails.Rows[0]["place_of_posting_id"]);
                        aspNetUser.placeOfPostingName = PersonalAndServiceDetails.Rows[0]["place_of_posting_name"].ToString();
                        aspNetUser.effective_from = Convert.ToDateTime(PersonalAndServiceDetails.Rows[0]["effective_from"]);
                        aspNetUser.is_active = Convert.ToBoolean(PersonalAndServiceDetails.Rows[0]["is_active"]);
                        aspNetUser.Id = user.Id;
                        aspNetUser.UserName = user.UserName;
                        aspNetUser.Email = user.Email;
                        aspNetUser.UserStatus = user.UserStatus;
                        DataTable userCompanyRoleMapping = await _bIAuthService.GetCompanyRolesMappingByUserId(user.Id);
                        List<AspNetUserRoles_Register_DTO> aspNetUserRoles = new List<AspNetUserRoles_Register_DTO>();
                        for (int i = 0; i < userCompanyRoleMapping.Rows.Count; i++)
                        {
                            AspNetUserRoles_Register_DTO UserRoles = new AspNetUserRoles_Register_DTO();
                            UserRoles.CompanyName = userCompanyRoleMapping.Rows[i]["company_name"].ToString();
                            UserRoles.RoleName = userCompanyRoleMapping.Rows[i]["Name"].ToString();
                            aspNetUserRoles.Add(UserRoles);
                        }
                        aspNetUser.Roles = aspNetUserRoles;
                        aspNetUsersList.Add(aspNetUser);

                    }
                }
            }
            return aspNetUsersList;

        }

        [HttpPost("ChangePassword/{userId}/{companyName}")]
        public async Task<object> ChangePassword([FromBody] ChangePassword Data, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin','Employee','Company Head','Manager'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            string errors = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            Data.UpdatedOn = DateTime.Now;

            if (Data.newPassword == Data.confirmPassword)
            {
                var userList = await _userManager.FindByNameAsync(Data.userName);
                var oldPassword = await _signInManager.PasswordSignInAsync(userList.UserName, Data.OldPassword, false, false);
                if (oldPassword.Succeeded)
                {
                    var result = await _userManager.ChangePasswordAsync(userList, Data.OldPassword, Data.newPassword);
                    if (result.Succeeded)
                    {
                        await _emailSender.SendEmailAsync(companyName, userList.Email, "HRMS Change Password",
                       "<b>Hello</b>,<br><br> <b> You have changed the password successfully HRMS </b><br>");
                        return Ok(new { message = "Password Change Successfully", messageDescription = "", messageType = "success" });
                    }
                    else
                    {
                        return Ok(new { message = "Password Change Failed", messageDescription = "", messageType = "error" });
                    }
                }
                else
                {
                    return Ok(new { message = "Old Password Doesn't Match", messageDescription = "", messageType = "error" });
                }

            }
            else
            {
                return Ok(new { message = "Password and Confirm Password Doesn't Match", messageDescription = "", messageType = "error" });
            }

        }

        [HttpPost("CreateRole/{userId}/{companyName}")]
        [Authorize]
        public async Task<object> CreateRoles([FromBody] AspNetRoles_DTO model, string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string errors = null;
            var userRoleExist = await _roleManager.RoleExistsAsync(model.Name);
            if (userRoleExist)
            {
                return Ok(new { message = "Already Role Exist", messageDescription = "", messageType = "error" });

            }

            var role = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = model.Name.Trim(), NormalizedName = model.Name.Trim().ToUpper() };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role Created Successfully", messageDescription = "", messageType = "success" });
            }
            else
            {
                return BadRequest(new { message = "Error While Trying To Add a Role", messageDescription = "", messageType = "error" });
            }

        }

        [HttpPut("EditRole/{userId}/{companyName}")]
        [Authorize]
        public async Task<object> UpdateRoles(AspNetRoles_DTO model, string userId, string companyName)
        {
            string errors = null;
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (model.Id == null)
                return BadRequest();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var roleExist = await _roleManager.FindByIdAsync(model.Id);
            if (roleExist == null)
            {
                return BadRequest(new { message = "No Such Role Found", messageDescription = "", messageType = "error" });
            }
            var roleNameExist = await _roleManager.FindByNameAsync(model.Name);
            if (roleNameExist != null)
            {
                if (roleNameExist.Id != model.Id)
                {
                    return Ok(new { message = "Already Role Exist", messageDescription = "", messageType = "error" });
                }
            }
            if (roleExist.Name != model.Name)
            {
                roleExist.Name = model.Name;

                var result = await _roleManager.UpdateAsync(roleExist);
                if (result.Succeeded)
                {
                    return Ok(new { message = "Role Updated Successfully", messageDescription = "", messageType = "success" });
                }
                else
                {
                    return BadRequest(new { message = "Error While Trying To Update a Role", messageDescription = "", messageType = "error" });

                }
            }
            else
            {
                return Ok(new { message = "Role Upto dated", messageDescription = "", messageType = "success" });
            }
        }

        [HttpGet("GetRoles/{userId}/{companyName}")]
        [Authorize]
        public async Task<object> GetRoles(string userId, string companyName)
        {
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Admin','Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            return _roleManager.Roles;
        }

        [HttpDelete("DeleteRole/{userId}/{companyName}/{id}")]
        [Authorize]
        public async Task<object> DeleteRoles(string userId, string companyName, string id)
        {
            string errors = null;
            var userCompanyRoleValidate = await _authoriseRoles.AuthorizeUserRole(userId, companyName, "'Super Admin'", _roleManager, _userManager);
            if (!userCompanyRoleValidate)
            {
                return BadRequest(new { message = "Unauthorize User.", messageDescription = "You are not authorize to use the module. Please contact with your admin for the permission" });
            }
            if (id == null)
                return BadRequest();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var roleExist = await _roleManager.FindByIdAsync(id);
            if (roleExist == null)
            {
                return BadRequest(new { message = "No Such Role Found", messageDescription = "", messageType = "error" });
            }
            var result = await _roleManager.DeleteAsync(roleExist);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role Deleted Successfully", messageDescription = "", messageType = "success" });
            }
            else
            {
                return BadRequest(new { message = "Error While Trying To Delete a Role", messageDescription = "", messageType = "error" });
            }

        }


        [HttpPost("UserCompanyRoleMapping/{userId}/{companyName}")]
        public async Task<IActionResult> UserCompanyRoleMapping(List<AspNetUserRoles_Register_DTO> data, string userId, string companyName)
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
            var result = await _bIAuthService.UserCompanyRoleMapping(data);
            if (result == "Succeeded")
            {
                return Ok(new { message = "User Company and Role Mapped Successfully", messageDescription = "", messageType = "success" });
            }
            else
            {
                return BadRequest(new { message = "Error While Trying To Map User Company and Role", messageDescription = "", messageType = "error" });
            }
        }

        [HttpGet("UserCompanyRoleAuth")]
        public async Task<IActionResult> UserCompanyRoleAuth(string UserId, string companyName, string RoleList)
        {
            return Ok(await _bIAuthService.GetCompanyRolesMapping(UserId, companyName, RoleList));
        }

        [HttpGet("GetRoleCompanyPermissionByCompanyandRole/{companyName}/{RoleName}")]
        public async Task<IActionResult> GetRoleCompanyPermissionByCompanyandRole(string companyName, string RoleName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bIAuthService.GetRoleCompanyPermissionByCompanyandRole(companyName, RoleName);
            return Ok(new { result = result });
        }

    }


}
