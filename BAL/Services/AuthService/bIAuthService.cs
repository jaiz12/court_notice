using DTO.Models;
using DTO.Models.Auth;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
// =============================================
// -- Author:		Jaideep Roy
// -- Create date: 09-Nov-2023
// =============================================
namespace BAL.Auth.AuthService
{
    public interface bIAuthService
    {
        Task<string> UserCompanyRoleMapping(List<AspNetUserRoles_Register_DTO> Data);
        Task<DataTable> GetCompanyRolesMapping(string UserId, string companyName, string RoleList);
        Task<DataTable> GetCompanyRolesMappingByUserId(string UserId);
        Task<DataTable> GetPersonalAndServiceDetailsByEmployeeId(string Id);
        Task<DataTable> GetRoleCompanyPermissionByCompanyandRole(string companyName, string RoleName);
        Task<DataResponse> DeleteUserCompanyRoleMapping(string id);

        Task<bool> ConfigEmployeeWiseLeave(AspNetUsers_Register_DTO registerUser);

        Task<List<LeaveConfiguration_DTO>> GetLeaveConfiguration(long company_id, long branch_id, long appointment_status_id, long financial_year_id);
    }
}
