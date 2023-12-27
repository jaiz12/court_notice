using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.EmployeeOperation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.EmployeeOperations.EmployeePromotionService
{
    public class EmployeePromotionService : MyDbContext, IEmployeePromotionService
    {
        public async Task<DataTable> GetEmployeeBasicServiceForPromotion(EmployeePromotionOptionValues_DTO promotionOptionValues)
        {
            try
            {
                OpenContext();

                var query = "select anu.UserName, anu.Email, " +
                    "epd.first_name, epd.middle_name, epd.last_name, esd.effective_from, esd.date_of_joining, esd.employee_service_id," +
                    "cm.company_id, cm.company_name, " +
                    "bm.branch_id, bm.branch_name, " +
                    "sm.state_id, sm.state_name, " +
                    "dm.district_id, dm.district_name, " +
                    "popm.place_of_posting_id, popm.place_of_posting_name, " +
                    "apm.appointment_status_id, apm.appointment_status_name, " +
                    "divm.division_id, divm.division_name, " +
                    "desm.designation_id, desm.designation_name " +
                    "from AspNetUsers as anu " +
                    "Join emp_employee_personal_details as epd on anu.UserName = epd.employee_id " +
                    "Join emp_employee_service_details as esd on  anu.UserName = esd.employee_id " +
                    "Join emp_company_master as cm on cm.company_id = esd.company_id " +
                    "Join emp_branch_master as bm on bm.branch_id = esd.branchoffice_id " +
                    "Join emp_state_master as sm on sm.state_id = esd.state_id " +
                    "Join emp_district_master as dm on dm.district_id = esd.district_id " +
                    "Join emp_place_of_posting_master as popm on popm.place_of_posting_id = esd.place_of_posting_id " +
                    "Join emp_appointment_status_master apm on apm.appointment_status_id = esd.appointment_status_id " +
                    "Join emp_division_master as divm on divm.division_id = esd.division_id " +
                    "Join emp_designation_master desm on desm.designation_id = esd.designation_id " +
                    "Where anu.UserStatus = 1 and esd.is_active = 1 ";
                if (promotionOptionValues.company_id != 0 && promotionOptionValues.company_id != null)
                {
                    query += " and esd.company_id = " + promotionOptionValues.company_id;
                }
                if (promotionOptionValues.branch_id != 0 && promotionOptionValues.branch_id != null)
                {
                    query += " and esd.branchoffice_id = " + promotionOptionValues.branch_id;
                }
                if (promotionOptionValues.state_id != 0 && promotionOptionValues.state_id != null)
                {
                    query += " and esd.state_id = " + promotionOptionValues.state_id;
                }
                if (promotionOptionValues.district_id != 0 && promotionOptionValues.district_id != null)
                {
                    query += " and esd.district_id = " + promotionOptionValues.district_id;
                }
                if (promotionOptionValues.place_of_posting_id != 0 && promotionOptionValues.place_of_posting_id != null)
                {
                    query += " and esd.place_of_posting_id = " + promotionOptionValues.place_of_posting_id;
                }
                if (promotionOptionValues.appointment_status_id != 0 && promotionOptionValues.appointment_status_id != null)
                {
                    query += " and esd.appointment_status_id = " + promotionOptionValues.appointment_status_id;
                }
                query += " order by esd.updated_on desc ";
                DataTable GetEmpServiceDetails = _sqlCommand.Select_Table(query, CommandType.Text);
                return GetEmpServiceDetails;
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
        public async Task<DataResponse> PromoteEmployeeDesignation(PromotedEmployeeService_DTO promote)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_employee_service_id", promote.employee_service_id);
                _sqlCommand.Add_Parameter_WithValue("prm_employee_id", promote.employee_id);
                _sqlCommand.Add_Parameter_WithValue("prm_designation_id", promote.designation_id);

                _sqlCommand.Add_Parameter_WithValue("prm_company_id", promote.company_id);
                _sqlCommand.Add_Parameter_WithValue("prm_branchoffice_id", promote.branchoffice_id);
                _sqlCommand.Add_Parameter_WithValue("prm_state_id", promote.state_id);
                _sqlCommand.Add_Parameter_WithValue("prm_district_id", promote.district_id);
                _sqlCommand.Add_Parameter_WithValue("prm_division_id", promote.division_id);
                _sqlCommand.Add_Parameter_WithValue("prm_appointment_status_id", promote.appointment_status_id);
                _sqlCommand.Add_Parameter_WithValue("prm_exit_type_id", promote.exit_type_id);
                _sqlCommand.Add_Parameter_WithValue("prm_date_of_joining", promote.date_of_joining);
                _sqlCommand.Add_Parameter_WithValue("prm_place_of_posting_id", promote.place_of_posting_id);
                _sqlCommand.Add_Parameter_WithValue("prm_effective_from", promote.effective_from = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_effective_to", promote.effective_to = DateTime.MaxValue);
                _sqlCommand.Add_Parameter_WithValue("prm_is_active", promote.is_active = true);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", promote.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", promote.created_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", promote.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", promote.updated_on = DateTime.Now);

                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_employee_promotion", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Employee Designation is Promoted Successfully", true);
                else
                    return new DataResponse("Failed To Update Employee Designation", false);
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
        public async Task<DataTable> GetEmployeePromotionHistory(EmployeePromotionOptionValues_DTO promotionOptionValues)
        {
            try
            {
                OpenContext();
                var query = "select anu.UserName, anu.Email, " +
                   "epd.first_name, epd.middle_name, epd.last_name, esd.effective_from, esd.effective_to, esd.date_of_joining, esd.employee_service_id, esd.is_active, " +
                   "cm.company_id, cm.company_name, " +
                   "bm.branch_id, bm.branch_name, " +
                   "sm.state_id, sm.state_name, " +
                   "dm.district_id, dm.district_name, " +
                   "popm.place_of_posting_id, popm.place_of_posting_name, " +
                   "apm.appointment_status_id, apm.appointment_status_name, " +
                   "divm.division_id, divm.division_name, " +
                   "desm.designation_id, desm.designation_name " +
                   "from AspNetUsers as anu " +
                   "Join emp_employee_personal_details as epd on anu.UserName = epd.employee_id " +
                   "Join emp_employee_service_details as esd on  anu.UserName = esd.employee_id " +
                   "Join emp_company_master as cm on cm.company_id = esd.company_id " +
                   "Join emp_branch_master as bm on bm.branch_id = esd.branchoffice_id " +
                   "Join emp_state_master as sm on sm.state_id = esd.state_id " +
                   "Join emp_district_master as dm on dm.district_id = esd.district_id " +
                   "Join emp_place_of_posting_master as popm on popm.place_of_posting_id = esd.place_of_posting_id " +
                   "Join emp_appointment_status_master apm on apm.appointment_status_id = esd.appointment_status_id " +
                   "Join emp_division_master as divm on divm.division_id = esd.division_id " +
                   "Join emp_designation_master desm on desm.designation_id = esd.designation_id " +
                   "Where anu.UserStatus = 1";
                if (promotionOptionValues.company_id != 0 && promotionOptionValues.company_id != null)
                {
                    query += " and esd.company_id = " + promotionOptionValues.company_id;
                }
                if (promotionOptionValues.branch_id != 0 && promotionOptionValues.branch_id != null)
                {
                    query += " and esd.branchoffice_id = " + promotionOptionValues.branch_id;
                }
                if (promotionOptionValues.state_id != 0 && promotionOptionValues.state_id != null)
                {
                    query += " and esd.state_id = " + promotionOptionValues.state_id;
                }
                if (promotionOptionValues.district_id != 0 && promotionOptionValues.district_id != null)
                {
                    query += " and esd.district_id = " + promotionOptionValues.district_id;
                }
                if (promotionOptionValues.place_of_posting_id != 0 && promotionOptionValues.place_of_posting_id != null)
                {
                    query += " and esd.place_of_posting_id = " + promotionOptionValues.place_of_posting_id;
                }
                if (promotionOptionValues.appointment_status_id != 0 && promotionOptionValues.appointment_status_id != null)
                {
                    query += " and esd.appointment_status_id = " + promotionOptionValues.appointment_status_id;
                }
                query += " order by esd.updated_on desc ";
                DataTable GetEmpServiceDetails = _sqlCommand.Select_Table(query, CommandType.Text);
                return GetEmpServiceDetails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseContext();
            }
        }
        public async Task<DataResponse> PutPromotedEmployeeDetails(EditPromotedEmployee_DTO dataModel)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_employee_service_id", dataModel.employee_service_id);
                _sqlCommand.Add_Parameter_WithValue("prm_employee_id", dataModel.employee_id);
                _sqlCommand.Add_Parameter_WithValue("prm_designation_id", dataModel.designation_id);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", dataModel.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", dataModel.updated_on = DateTime.Now);

                string query = $"UPDATE emp_employee_service_details " +
                                "SET " +
                                "designation_id = @prm_designation_id, " +
                                "updated_by = @prm_updated_by, " +
                                "updated_on = @prm_updated_on " +
                                "WHERE " +
                                "employee_service_id = @prm_employee_service_id ";

                var result = await Task.Run(() => _sqlCommand.Execute_Query(query, CommandType.Text));

                if (result)
                    return new DataResponse("Employee Promotion Updated Successfully", true);
                else
                    return new DataResponse("Failed To Update Employee Promotion", false);
            }
            catch (Exception ex)
            {
                return new DataResponse($"An error occured, {ex.Message}", false);
            }
            finally
            {
                CloseContext();
            }
        }
        public async Task<DataResponse> DeletePromotedEmployeeHistoryDetails(long employee_service_id)
        {
            System.Data.SqlClient.SqlTransaction Trans = null;
            try
            {
                OpenContext();
                Trans = _connection._Connection.BeginTransaction(IsolationLevel.Serializable);
                _sqlCommand.Add_Transaction(Trans);

               

                string getEmpId = $"Select employee_id from emp_employee_service_details   Where employee_service_id = {employee_service_id}";
                string employeeId = await Task.Run(() => _sqlCommand.Select_Scalar(getEmpId, CommandType.Text));

                string getRecCount = $"Select count(employee_id) employee_service_id from emp_employee_service_details where employee_id ='{employeeId}' order by employee_service_id desc";

                string EmpCount = await Task.Run(() => _sqlCommand.Select_Scalar(getRecCount, CommandType.Text));

                if (Convert.ToInt16(EmpCount) > 1)
                {

                    string deleteQuery = $"Delete from emp_employee_service_details Where employee_service_id = {employee_service_id}";
                    string selectQuery = $"Select top 1 employee_service_id from emp_employee_service_details where employee_id ='{employeeId}' order by employee_service_id desc";

                    var deleteEmployeeService = await Task.Run(() => _sqlCommand.Execute_Query(deleteQuery, CommandType.Text));
                    DataTable selectEmployeeDT = await Task.Run(() => _sqlCommand.Select_Table(selectQuery, CommandType.Text));


                    string updateQuery = $"Update emp_employee_service_details Set is_active = 1, effective_to =  '{DateTime.MaxValue.ToString("yyyy-MM-dd")}' Where  employee_service_id = {selectEmployeeDT.Rows[0]["employee_service_id"]}";
                    var updateEmployeeService = await Task.Run(() => _sqlCommand.Execute_Query(updateQuery, CommandType.Text));
                    Trans.Commit();

                    if (updateEmployeeService)
                    {
                        return new DataResponse("Employee Promotion Deleted Successfully", true);
                    }
                    else
                    {
                        return new DataResponse("Failed To Delete Employee Promotion", false);
                    }
                }
                else
                {
                    return new DataResponse("Can't Delete Singular Record", true);
                }
                
               

                
            }
            catch (Exception ex)
            {
                Trans.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseContext();
            }
        }
    }
}
