using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DTO.Models;
using DTO.Models.Auth;
using System.Data;

namespace BAL.Services.Auth
{
    public interface IAuthService
    {
        Task<DataResponse> CreateUser(AspNetUsers_Register_DTO register_DTO);

        Task<DataResponse> EditUser(AspNetUsers_Edit_DTO register_DTO);
        Task<object> GetUsers();
        Task<DataResponse> DeleteUser(string user_id);
        Task<DataTable> GetRoles();
    }
}
