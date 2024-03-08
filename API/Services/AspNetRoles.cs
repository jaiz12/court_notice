using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IAspNetRoles
    {
        Task<bool> AspNetRole(System.Data.DataTable roles);
    }
    public class AspNetRoles : IAspNetRoles
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AspNetRoles(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> AspNetRole(System.Data.DataTable roles)
        {
           
            bool resultType = false;
            try
            {
                for (int i = 0; i < roles.Rows.Count; i++)
                {
                    if (!await _roleManager.RoleExistsAsync(roles.Rows[i]["rolename"].ToString()))
                    {
                        var result = await _roleManager.CreateAsync(new IdentityRole(roles.Rows[i]["rolename"].ToString().Trim()));
                        resultType = true;
                    }
                    else if(await _roleManager.RoleExistsAsync(roles.Rows[i]["rolename"].ToString()))
                    {
                        resultType = true;
                    }
                }
            }
            catch
            {
                resultType = false;
            }
            return resultType;
        }
    }
}
