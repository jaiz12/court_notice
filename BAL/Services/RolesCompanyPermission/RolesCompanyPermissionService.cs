using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Auth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
// =============================================
// -- Author:		Jaideep Roy
// -- Create date: 17-Nov-2023
// =============================================
namespace BAL.Services.RolesCompanyPermission
{
    public class RolesCompanyPermissionService : MyDbContext, IRolesCompanyPermissionService
    {
        public async Task<DataResponse> PostRoleCompanyPermission(RoleCompanyPermission_DTO modal)
        {
            try
            {
                OpenContext();
                var roleCompanyPermissionExist = $"Select * from RolePermissionMapping where Name = '{modal.Name}'";
                DataTable roleCompanyExist = await Task.Run(() => _sqlCommand.Select_Table(roleCompanyPermissionExist, CommandType.Text));
                if (roleCompanyExist.Rows.Count == 0)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue("prm_name", modal.Name);
                    _sqlCommand.Add_Parameter_WithValue("prm_permission", modal.Permission);
                    _sqlCommand.Add_Parameter_WithValue("prm_created_by", modal.CreatedBy);
                    _sqlCommand.Add_Parameter_WithValue("prm_created_on", DateTime.Now);
                    _sqlCommand.Add_Parameter_WithValue("prm_updated_by", modal.UpdatedBy);
                    _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                    var item = await Task.Run(() => _sqlCommand.Execute_Query("post_role_company_permission", CommandType.StoredProcedure));

                    if (item)
                        return new DataResponse("Role Company Permission Added Successfully", true);
                    else
                        return new DataResponse("Role Company Permission Can't Add Successfully", false);
                }
                else
                {
                    return new DataResponse("Role Company Permission Already Exists", false);
                }
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

        public async Task<DataResponse> UpdateRoleCompanyPermission(RoleCompanyPermission_DTO modal)
        {
            try
            {
                OpenContext();

                string message = "";
                bool status = false;

                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_id", modal.Id);
                _sqlCommand.Add_Parameter_WithValue("prm_name", modal.Name);
                _sqlCommand.Add_Parameter_WithValue("prm_permission", modal.Permission);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", modal.UpdatedBy);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", DateTime.Now);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("update_role_company_permission", CommandType.StoredProcedure));

                if (item)
                {
                    message = "Role Company Permission successfully Updated";
                    status = true;
                }

                else
                {
                    message = "Role Company Permission Update failed";
                    status = false;
                }


                return new DataResponse(message, status);

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

        public async Task<List<RoleCompanyPermission_DTO>> GetRoleCompanyPermission()
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                DataTable roleCompanyPermissionDT = await _sqlCommand.ExecuteStoredProcedureAsync("get_role_company_permission", CommandType.StoredProcedure);
                List<RoleCompanyPermission_DTO> roleCompanyPermission = new List<RoleCompanyPermission_DTO>();
                roleCompanyPermission = DataTableVsListOfType.ConvertDataTableToList<RoleCompanyPermission_DTO>(roleCompanyPermissionDT);
                return roleCompanyPermission;
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


        public async Task<DataResponse> DeleteRoleCompanyPermission(long id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("@prm_id", id);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("delete_role_company_permission", CommandType.StoredProcedure));
                if (item)
                    return new DataResponse("Role Company Permission Deleted Successfully", true);
                else
                    return new DataResponse("Role Company Permission Delete Failed", false);
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


    }
}
