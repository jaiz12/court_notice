using BAL.Auth.AuthService;
using DTO.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IAuthoriseRoles
    {
        Task<bool> AuthorizeUserRole(string userId, string companyName, string RoleList, RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager);
    }
    public class AuthoriseRoles : IAuthoriseRoles
    {
        private readonly bIAuthService _bIAuthService;

        public AuthoriseRoles(bIAuthService bIAuthService)
        {
            _bIAuthService = bIAuthService;
        }

        public async Task<bool> AuthorizeUserRole(string userId, string companyName, string RoleList, RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager)
        {
            try
            {
                bool result = false;

                var user = _userManager.FindByIdAsync(userId).Result;
                if (user == null)
                {
                    result = false;

                }
                else
                {
                    DataTable RoleListCount = await _bIAuthService.GetCompanyRolesMapping(userId, companyName, RoleList);
                    if (RoleListCount.Rows.Count <= 0)
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }

    }
}
