using Common.DataContext;
using Common.DbContext;
using DTO.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DTO.Models;
using System.Data.SqlClient;
using System.Data;
using Common.Utilities;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace BAL.Services.Auth
{
    public class AuthService : MyDbContext, IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(
           UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<DataResponse> CreateUser(AspNetUsers_Register_DTO register_DTO)
        {
            System.Data.SqlClient.SqlTransaction Trans = null;
            try
            {
                OpenContext();
                Trans = _connection._Connection.BeginTransaction(IsolationLevel.Serializable);
                _sqlCommand.Add_Transaction(Trans);
                register_DTO.Id = Guid.NewGuid().ToString();
                var user = new ApplicationUser()
                {
                    Id = register_DTO.Id,
                    UserName = register_DTO.Email,
                    Email = register_DTO.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    NormalizedUserName = register_DTO.UserName.ToUpper(),
                    NormalizedEmail = register_DTO.Email.ToUpper(),
                    PhoneNumber = register_DTO.PhoneNumber,
                    UserStatus = true,
                    CreatedOn = DateTime.Now,
                    CreatedBy = register_DTO.CreatedBy,
                    UpdatedBy = register_DTO.UpdatedBy,
                    UpdatedOn = DateTime.Now
                };
                var result = await _userManager.CreateAsync(user, register_DTO.Password);
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_user_id", register_DTO.Id);
                _sqlCommand.Add_Parameter_WithValue("prm_user_name", register_DTO.UserName);
                _sqlCommand.Add_Parameter_WithValue("prm_designation_id", register_DTO.designation_id);
                _sqlCommand.Add_Parameter_WithValue("prm_department_id", register_DTO.department_id);
                _sqlCommand.Add_Parameter_WithValue("prm_address", register_DTO.Address);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", register_DTO.CreatedBy);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", register_DTO.UpdatedBy);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("post_userDetails", CommandType.StoredProcedure));
                Trans.Commit();
                if (item)
                {
                    foreach (var role in register_DTO.Roles)
                    {
                        await _userManager.AddToRoleAsync(user, role.rolename);
                    }
                    return new DataResponse("User Registered Successfully", true);
                }
                else
                {
                    return new DataResponse("User Registered Unsuccessfully", false);
                }

            }
            catch (Exception ex)
            {
                Trans.Rollback();
                return new DataResponse("User Registered Unsuccessfully", false);
            }
            finally
            {
                CloseContext();
            }

        }


        public async Task<DataResponse> EditUser(AspNetUsers_Edit_DTO register_DTO)
        {
            System.Data.SqlClient.SqlTransaction Trans = null;
            try
            {
                OpenContext();
                Trans = _connection._Connection.BeginTransaction(IsolationLevel.Serializable);
                _sqlCommand.Add_Transaction(Trans);
                var user = await _userManager.FindByIdAsync(register_DTO.Id);
                user.UserName = register_DTO.Email;
                user.Email = register_DTO.Email;
                user.NormalizedUserName = register_DTO.UserName.ToUpper();
                user.NormalizedEmail = register_DTO.Email.ToUpper();
                user.PhoneNumber = register_DTO.PhoneNumber;
                user.UserStatus = register_DTO.UserStatus;
                user.UpdatedBy = register_DTO.UpdatedBy;
                user.UpdatedOn = DateTime.Now;
                var result = await _userManager.UpdateAsync(user);              

                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_user_id", register_DTO.Id);
                _sqlCommand.Add_Parameter_WithValue("prm_user_name", register_DTO.UserName);
                _sqlCommand.Add_Parameter_WithValue("prm_designation_id", register_DTO.designation_id);
                _sqlCommand.Add_Parameter_WithValue("prm_department_id", register_DTO.department_id);
                _sqlCommand.Add_Parameter_WithValue("prm_address", register_DTO.Address);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", register_DTO.UpdatedBy);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("update_userDetails", CommandType.StoredProcedure));
                Trans.Commit();
                if (item)
                {
                    await _userManager.RemoveFromRoleAsync(user, register_DTO.Id);
                    foreach (var role in register_DTO.Roles)
                    {
                        await _userManager.AddToRoleAsync(user, role.rolename);
                    }
                    return new DataResponse("User Updated Successfully", true);
                }
                else
                {
                    return new DataResponse("User Updated Unsuccessfully", false);
                }

            }
            catch (Exception ex)
            {
                Trans.Rollback();
                return new DataResponse("User Updated Unsuccessfully", false);
            }
            finally
            {
                CloseContext();
            }

        }

        public async Task<object> GetUsers()
        {
            try
            {
                OpenContext();
                var users = _sqlCommand.Select_Table("get_users", CommandType.StoredProcedure);
                var userRoles = _sqlCommand.Select_Table("get_users_roles", CommandType.StoredProcedure);
                var usersList = DataTableVsListOfType.ConvertDataTableToList<AspNetUsers_Register_DTO>(users);
                var userRolesList = DataTableVsListOfType.ConvertDataTableToList<AspNetUserRoles_Register_DTO>(userRoles);

                foreach (var user in usersList)
                {

                    List<AspNetUserRoles_Register_DTO> aspNetUserRoles = new List<AspNetUserRoles_Register_DTO>();
                    foreach (var userRole in userRolesList)
                    {
                        if (user.Id == userRole.UserId)
                        {
                            AspNetUserRoles_Register_DTO UserRole = new AspNetUserRoles_Register_DTO();
                            UserRole.UserId = userRole.UserId;
                            UserRole.RoleId = userRole.RoleId;
                            UserRole.Name = userRole.Name;
                            aspNetUserRoles.Add(UserRole);
                        }
                    }
                    user.Roles = aspNetUserRoles;

                }
                return usersList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { CloseContext(); }
        }



        public async Task<DataResponse> DeleteUser(string user_id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_user_id", user_id);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("delete_user", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("User Deleted Successfully", true);
                else
                    return new DataResponse("Failed To Delete User", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }

        public async Task<DataTable> GetRoles()
        {

            try
            {
                OpenContext();
                var query = "Select * from tbl_rolemaster";
                var result = _sqlCommand.Select_Table(query, CommandType.Text);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { CloseContext(); }
        }

       
    }
}
