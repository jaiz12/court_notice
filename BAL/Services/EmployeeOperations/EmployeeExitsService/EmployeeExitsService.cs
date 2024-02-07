using Common.DbContext;
using DTO.Models;
using DTO.Models.EmployeeOperation.Exits;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BAL.Services.EmployeeOperations.EmployeeExitsService
{
    public class EmployeeExitsService : MyDbContext, IEmployeeExitsService
    {
        public async Task<DataTable> GetEmployeeBasicDetailsForExits(EmployeeExitsOptionValue_DTO _optionValue)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_company_id", _optionValue.company_id);
                _sqlCommand.Add_Parameter_WithValue("prm_branch_id", _optionValue.branch_id);
                _sqlCommand.Add_Parameter_WithValue("prm_state_id", _optionValue.state_id);
                _sqlCommand.Add_Parameter_WithValue("prm_district_id", _optionValue.district_id);
                _sqlCommand.Add_Parameter_WithValue("prm_place_of_posting_id", _optionValue.place_of_posting_id);
                _sqlCommand.Add_Parameter_WithValue("prm_appointment_status_id", _optionValue.appointment_status_id);
                DataTable getEmployeeBasicDetailsForExitsDT = await Task.Run (() => _sqlCommand.Select_Table("emp_get_employee_servicedetails_for_exits", CommandType.StoredProcedure));
                return getEmployeeBasicDetailsForExitsDT;
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
        public async Task<DataResponse> PutEmployeeExits(EmployeeExits_DTO _exit)
        {
            OpenContext();
            SqlTransaction sqlTransaction = _connection._Connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            _sqlCommand.Add_Transaction(sqlTransaction);
            try
            {
                if (_exit.button_id == 1)
                {
                    string updateQueryEmpService = $"Update emp_employee_service_details Set effective_to = '{Convert.ToDateTime(_exit.effective_to).ToString("yyyy-MM-dd")}' Where employee_service_id = {_exit.employee_service_id}";
                    bool updateEmployeeService = await Task.Run(() => _sqlCommand.Execute_Query(updateQueryEmpService, CommandType.Text));

                    string updateQueryEmpAspNetUsers = $"Update AspNetUsers Set UserStatus = '{_exit.UserStatus}' Where UserName = '{_exit.UserName}'";
                    bool updateAspNetUsers = await Task.Run(()=> _sqlCommand.Execute_Query(updateQueryEmpAspNetUsers, CommandType.Text));
                    sqlTransaction.Commit();

                    if(updateEmployeeService && updateAspNetUsers)
                    {
                        return new DataResponse("Employee Exited Successfully", true);
                    }
                    else
                    {
                        return new DataResponse("Failed To Exit Employee", false);
                    }
                }
                else if (_exit.button_id == 2)
                {
                    string updateQueryEmpService = $"Update emp_employee_service_details Set effective_to = '{Convert.ToDateTime(_exit.effective_to = DateTime.MaxValue).ToString("yyyy-MM-dd")}' Where employee_service_id = {_exit.employee_service_id}";
                    bool updateEmployeeService = await Task.Run(() => _sqlCommand.Execute_Query(updateQueryEmpService, CommandType.Text));

                    string updateQueryEmpAspNetUsers = $"Update AspNetUsers Set UserStatus = '{_exit.UserStatus}' Where UserName = '{_exit.UserName}'";
                    bool updateAspNetUsers = await Task.Run(() => _sqlCommand.Execute_Query(updateQueryEmpAspNetUsers, CommandType.Text));
                    sqlTransaction.Commit();

                    if (updateEmployeeService && updateAspNetUsers)
                    {
                        return new DataResponse("Employee Exited Reset Successfully", true);
                    }
                    else
                    {
                        return new DataResponse("Failed To Exit Reset Employee", false);
                    }
                }
                else
                {
                    return new DataResponse("An Error Occured, Failed to Exit Employee", false);
                }
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }
    }
}
