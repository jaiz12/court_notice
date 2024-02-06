using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.EmployeeOperation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BAL.Services.EmployeeOperations.EmployeeAwardService
{
    public class EmployeeAwardService : MyDbContext, IEmployeeAwardService
    {
        public async Task<DataTable> GetEmployeeBasicServiceForAward(string username)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("UserName", username);
                DataTable result = await Task.Run(() => _sqlCommand.Select_Table("emp_get_employee_servicedetails_for_award_by_employee_id", CommandType.StoredProcedure));
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
            finally
            {
                CloseContext();
            }
        }
        public async Task<DataResponse> PostEmployeeAward(EmployeeAward_DTO employeeAward)
        {
            try
            {
                OpenContext();

                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_employee_award_name", Regex.Replace(employeeAward.employee_award_name.Trim(), @"\s+", " "));
                _sqlCommand.Add_Parameter_WithValue("prm_employee_award_date", employeeAward.employee_award_date);
                _sqlCommand.Add_Parameter_WithValue("prm_employee_award_month", employeeAward.employee_award_month);
                _sqlCommand.Add_Parameter_WithValue("prm_employee_id", employeeAward.employee_id.ToUpper());

                string query = $"select employee_id, employee_award_date, employee_award_name " +
                               $"from emp_employee_awards " +
                               $"where employee_award_date = @prm_employee_award_date " +
                               $"and employee_award_name = @prm_employee_award_name " +
                               $"and employee_id = @prm_employee_id";
                DataTable checkAwardNameDateDT = _sqlCommand.Select_Table(query, CommandType.Text);
                if (checkAwardNameDateDT.Rows.Count > 0)
                {
                    return new DataResponse("Employee Award Already Exist", false);
                }

                _sqlCommand.Add_Parameter_WithValue("prm_award_description", employeeAward.award_description == null ? null : Regex.Replace(employeeAward.award_description.Trim(), @"\s+", " "));
                _sqlCommand.Add_Parameter_WithValue("prm_employee_name", employeeAward.employee_name);

                _sqlCommand.Add_Parameter_WithValue("prm_company_name", employeeAward.company_name);
                _sqlCommand.Add_Parameter_WithValue("prm_state_name", employeeAward.state_name);
                _sqlCommand.Add_Parameter_WithValue("prm_division_name", employeeAward.division_name);
                _sqlCommand.Add_Parameter_WithValue("prm_place_of_posting_name", employeeAward.place_of_posting_name);
                _sqlCommand.Add_Parameter_WithValue("prm_designation_name", employeeAward.designation_name);
                _sqlCommand.Add_Parameter_WithValue("pm_created_on", employeeAward.created_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", employeeAward.updated_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", employeeAward.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", employeeAward.created_by);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_employee_award", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Employee Award Added Successfully", true);
                else
                    return new DataResponse("Employee Code Already Exists", false);
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
        public async Task<List<EmployeeAward_DTO>> GetEmployeeAward(EmpAwardFilter_DTO filters)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_company_name", filters.company_name);
                _sqlCommand.Add_Parameter_WithValue("prm_state_name", filters.state_name);
                _sqlCommand.Add_Parameter_WithValue("prm_place_of_posting_name", filters.place_of_posting_name);
                _sqlCommand.Add_Parameter_WithValue("prm_designation_name", filters.designation_name);
                _sqlCommand.Add_Parameter_WithValue("prm_division_name", filters.division_name);
                var result = _sqlCommand.Select_Table("emp_get_employee_award", CommandType.StoredProcedure);
                return DataTableVsListOfType.ConvertDataTableToList<EmployeeAward_DTO>(result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                CloseContext();
            }
        }
        public async Task<DataResponse> PutEmployeeAward(EmployeeAwardEdit_DTO employeeAward)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue("prm_employee_id", employeeAward.employee_id.ToUpper());
                _sqlCommand.Add_Parameter_WithValue("prm_employee_award_date", employeeAward.employee_award_date);
                _sqlCommand.Add_Parameter_WithValue("prm_employee_award_name", Regex.Replace(employeeAward.employee_award_name.Trim(), @"\s+", " "));

                string query = $"select [employee_award_id], employee_id, employee_award_date, employee_award_name " +
                               $"from emp_employee_awards " +
                               $"where employee_award_date = @prm_employee_award_date " +
                               $"and employee_award_name = @prm_employee_award_name " +
                               $"and employee_id = @prm_employee_id";
                DataTable checkAwardNameDateDT = _sqlCommand.Select_Table(query, CommandType.Text);
                if (checkAwardNameDateDT.Rows.Count > 0 && Convert.ToInt64(checkAwardNameDateDT.Rows[0]["employee_award_id"]) != employeeAward.employee_award_id)
                {
                    return new DataResponse("Employee Award Already Exist", false);
                }

                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue("prm_employee_award_name", Regex.Replace(employeeAward.employee_award_name.Trim(), @"\s+", " "));
                _sqlCommand.Add_Parameter_WithValue("prm_employee_award_date", employeeAward.employee_award_date);
                _sqlCommand.Add_Parameter_WithValue("prm_employee_award_month", employeeAward.employee_award_month);

                _sqlCommand.Add_Parameter_WithValue("prm_employee_award_id", employeeAward.employee_award_id);

                _sqlCommand.Add_Parameter_WithValue("prm_award_description", employeeAward.award_description == null ? null : Regex.Replace(employeeAward.award_description.Trim(), @"\s+", " "));

                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", employeeAward.updated_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", employeeAward.updated_by);

                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_employee_award", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Employee Award Updated Successfully", true);
                else
                    return new DataResponse("Employee Code Already Exists", false);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
            finally
            {
                CloseContext();
            }
        }
        public async Task<DataResponse> DeleteEmployeeAward(long employee_award_id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_employee_award_id", employee_award_id);
                var item = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_employee_award", CommandType.StoredProcedure));

                if (item)
                    return new DataResponse("Employee Award Added Successfully", true);
                else
                    return new DataResponse("Failed To Delete Employee Award", false);

            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}", ex);
            }
            finally
            {
                CloseContext();
            }
        }
        public async Task<DataTable> GetEmployeeDetailsForAward(EmployeeAwardFilter_DTO filters)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();

                _sqlCommand.Add_Parameter_WithValue("prm_company_id", filters.company_id);
                _sqlCommand.Add_Parameter_WithValue("prm_state_id", filters.state_id);
                _sqlCommand.Add_Parameter_WithValue("prm_district_id", filters.district_id);
                _sqlCommand.Add_Parameter_WithValue("prm_branch_id", filters.branch_id);
                _sqlCommand.Add_Parameter_WithValue("prm_place_of_posting_id", filters.place_of_posting_id);
                _sqlCommand.Add_Parameter_WithValue("prm_appointment_status_id", filters.appointment_status_id);

                DataTable GetEmployeeDetailsDT = _sqlCommand.Select_Table("emp_get_employee_servicedetails_for_award", CommandType.StoredProcedure);
                return GetEmployeeDetailsDT;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured, {ex.Message}");
            }
            finally
            {
                CloseContext();
            }
        }
    }
}
