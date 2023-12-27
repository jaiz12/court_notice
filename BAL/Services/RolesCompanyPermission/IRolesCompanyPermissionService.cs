using DTO.Models;
using DTO.Models.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;
// =============================================
// -- Author:		Jaideep Roy
// -- Create date: 17-Nov-2023
// =============================================
namespace BAL.Services.RolesCompanyPermission
{
    public interface IRolesCompanyPermissionService
    {
        Task<DataResponse> PostRoleCompanyPermission(RoleCompanyPermission_DTO modal);
        Task<DataResponse> UpdateRoleCompanyPermission(RoleCompanyPermission_DTO modal);
        Task<List<RoleCompanyPermission_DTO>> GetRoleCompanyPermission();
        Task<DataResponse> DeleteRoleCompanyPermission(long id);

    }
}
