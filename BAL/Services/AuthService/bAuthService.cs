using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Auth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
// =============================================
// -- Author:		Jaideep Roy
// -- Create date: 09-Nov-2023
// =============================================
namespace BAL.Auth.AuthService
{
    public class bAuthService : MyDbContext, bIAuthService
    {
        public async Task<string> UserCompanyRoleMapping(List<AspNetUserRoles_Register_DTO> modal)
        {
            try
            {
                OpenContext();
                string message = null;
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_UserId", modal[0].UserId);
                await Task.Run(() => _sqlCommand.Execute_Query("delete_User_Company_Role_Mapping", CommandType.StoredProcedure));
                foreach (var data in modal)
                {
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue("prm_UserId", data.UserId);
                    _sqlCommand.Add_Parameter_WithValue("prm_roleName", data.RoleName);
                    _sqlCommand.Add_Parameter_WithValue("prm_companyName", data.CompanyName);


                    var item = await Task.Run(() => _sqlCommand.Execute_Query("post_User_Company_Role_Mapping", CommandType.StoredProcedure));
                    if (item)
                        message = "Succeeded";
                    else
                        message = "UnSucceeded";
                }
                return message;


            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            finally
            {
                CloseContext();
            }
        }

        public async Task<DataTable> GetCompanyRolesMapping(string UserId, string companyName, string RoleList)
        {
            //var ConnectiobDetails = OpenContext1();
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                var query = $"SELECT *  FROM [UserCompanyRoleMapping]   where [UserId] = '{UserId}' and [company_name] = '{companyName}' and [Name] in ({RoleList})";

                var result = await Task.Run(() => _sqlCommand.Select_Table(query, CommandType.Text));
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //CloseContext1(ConnectiobDetails.Item1, ConnectiobDetails.Item2);
                CloseContext();
            }
        }

        public async Task<DataTable> GetCompanyRolesMappingByUserId(string UserId)
        {
            //var ConnectiobDetails = OpenContext1();
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                var query = $"SELECT *  FROM [UserCompanyRoleMapping]  where [UserId] = '{UserId}'";

                var result = await Task.Run(() => _sqlCommand.Select_Table(query, CommandType.Text));
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //CloseContext1(ConnectiobDetails.Item1, ConnectiobDetails.Item2);
                CloseContext();
            }
        }

        public async Task<DataTable> GetPersonalAndServiceDetailsByEmployeeId(string Id)
        {
            //var ConnectiobDetails = OpenContext1();
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("UserId", Id);
                var result = await Task.Run(() => _sqlCommand.Select_Table("emp_get_employee_personal_and_sevice_details", CommandType.StoredProcedure));
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //CloseContext1(ConnectiobDetails.Item1, ConnectiobDetails.Item2);
                CloseContext();
            }
        }


        public async Task<DataTable> GetRoleCompanyPermissionByCompanyandRole(string companyName, string RoleName)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                var query = $"SELECT * FROM RolePermissionMapping where CompanyName = '0' and Name = '{RoleName}'";
                var result = await Task.Run(() => _sqlCommand.Select_Table(query, CommandType.Text));
                result.Rows[0]["companyName"] = companyName;
                return result;
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

        public async Task<DataResponse> DeleteUserCompanyRoleMapping(string id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                var query = $"Delete FROM UserCompanyRoleMapping where UserId = '{id}'";

                var result = await Task.Run(() => _sqlCommand.Execute_Query(query, CommandType.Text));

                if (result)
                    return new DataResponse("UserCompanyRoleMapping Deleted Successfully", true);
                else
                    return new DataResponse("Failed To UserCompanyRoleMapping", false);
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

        public async Task<bool> ConfigEmployeeWiseLeave(AspNetUsers_Register_DTO registerUser)
        {
            try
            {
                OpenContext();
                DataTable financialYear = await Task.Run(() => _sqlCommand.Select_Table("Select * from lv_financial_year_master", CommandType.Text));
                var date = DateTime.Now;

                List<FinancialYear_DTO> financialYearList = new List<FinancialYear_DTO>();
                financialYearList = DataTableVsListOfType.ConvertDataTableToList<FinancialYear_DTO>(financialYear);
                var filteredFinancialYear = financialYearList.Where(financialYear => financialYear.from_date <= date && financialYear.to_date >= date);

                var fy_id = 0;
                var fy_endDate = DateTime.Now;
                foreach (var fy in filteredFinancialYear)
                {
                    fy_id = Convert.ToInt32(fy.financial_year_id);
                    fy_endDate = fy.to_date;
                }
                var leaveConfig = await GetLeaveConfiguration(registerUser.company, registerUser.branch, registerUser.appointmentStatus, fy_id);
                var filterLeave = leaveConfig.Where(leave => leave.gender_id == registerUser.gender || leave.gender_id == null);

                bool result = false;
                foreach (var leaves in filterLeave)
                {
                    var userName = registerUser.UserName;
                    _sqlCommand.Clear_CommandParameter();
                    _sqlCommand.Add_Parameter_WithValue("prm_employee_id", registerUser.UserName);
                    _sqlCommand.Add_Parameter_WithValue("prm_leave_type_id", leaves.leave_type_id);
                    long total_leave = 0;
                    if (leaves.yearly_carry_forward && leaves.partial_carry)
                    {
                        total_leave = leaves.count;
                    }
                    if (leaves.yearly_carry_forward && leaves.full_carry)
                    {
                        total_leave = leaves.max_leave_per_year;
                    }
                    if (leaves.monthly_carry_forward)
                    {
                        total_leave = leaves.increment_count;
                    }
                    else
                    {
                        total_leave = leaves.max_leave_per_year;
                    }

                    _sqlCommand.Add_Parameter_WithValue("prm_total_leave", total_leave);
                    _sqlCommand.Add_Parameter_WithValue("@prm_balance", total_leave);
                    _sqlCommand.Add_Parameter_WithValue("@prm_effective_from", registerUser.dateOfJoining);
                    _sqlCommand.Add_Parameter_WithValue("@prm_effective_to", Convert.ToDateTime(fy_endDate));
                    _sqlCommand.Add_Parameter_WithValue("prm_financial_year_id", fy_id);
                    _sqlCommand.Add_Parameter_WithValue("@prm_created_by", registerUser.CreatedBy);
                    _sqlCommand.Add_Parameter_WithValue("@prm_created_on", DateTime.Now);
                    _sqlCommand.Add_Parameter_WithValue("@prm_updated_by", registerUser.UpdatedBy);
                    _sqlCommand.Add_Parameter_WithValue("@prm_updated_on", DateTime.Now);
                    var item = await Task.Run(() => _sqlCommand.Execute_Query("lv_post_employee_wise_leave_config", CommandType.StoredProcedure));

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                CloseContext();
            }
        }

        public async Task<List<LeaveConfiguration_DTO>> GetLeaveConfiguration(long company_id, long branch_id, long appointment_status_id, long financial_year_id)
        {
                var query = $"Select * from lv_leave_configuration_master where company_id ={company_id} and branch_id = {branch_id} and appointment_status_id = {appointment_status_id} and financial_year_id = {financial_year_id};";
                DataTable leaveConfigDT = await Task.Run(() => _sqlCommand.Select_Table(query, CommandType.Text));
                List<LeaveConfiguration_DTO> leaveconfiglist = new List<LeaveConfiguration_DTO>();
                leaveconfiglist = DataTableVsListOfType.ConvertDataTableToList<LeaveConfiguration_DTO>(leaveConfigDT);
                return leaveconfiglist;
            
        }


    }
}
