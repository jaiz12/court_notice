using Common.DbContext;
using DTO.Models;
using DTO.Models.EmployeeOperation;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.EmployeeOperations.EmployeeExtensionService
{
    public class EmployeeExtension_TerminationService : MyDbContext, IEmployeeExtension_TerminationService
    {
        public async Task<DataTable> GetEmployeeServiceForExtensionTermination(EmployeeExtension_Termination_DTO termination)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                var query = "select * from (select anu.UserName, anu.Email, " +
                    "epd.first_name, epd.middle_name, epd.last_name, esd.effective_from, esd.effective_to   " +
                    ", esd.date_of_joining, esd.employee_service_id, esd.is_active, " +
                    "cm.company_id, cm.company_name, " +
                    "bm.branch_id, bm.branch_name, " +
                    "sm.state_id, sm.state_name, " +
                    "dm.district_id, dm.district_name, " +
                    "popm.place_of_posting_id, popm.place_of_posting_name, " +
                    "apm.appointment_status_id, apm.appointment_status_name, " +
                    "divm.division_id, divm.division_name, " +
                    "desm.designation_id, desm.designation_name, " +
                    "eset.service_extend_terminate_id,eset.is_active as esetActive, eset.updated_on as esetUpdatedOn, eset.service_effective_to, eset.remark " +
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
                    "Left Join emp_employee_service_extend_termination as eset on eset.employee_id = esd.employee_id " +
                    "Where anu.UserStatus = 1 and esd.is_active = 1 ";

                if (termination.company_id != 0 && termination.company_id != null)
                {
                    query += " and esd.company_id = " + termination.company_id;
                }
                if (termination.branch_id != 0 && termination.branch_id != null)
                {
                    query += " and esd.branchoffice_id = " + termination.branch_id;
                }
                if (termination.state_id != 0 && termination.state_id != null)
                {
                    query += " and esd.state_id = " + termination.state_id;
                }
                if (termination.district_id != 0 && termination.district_id != null)
                {
                    query += " and esd.district_id = " + termination.district_id;
                }
                if (termination.place_of_posting_id != 0 && termination.place_of_posting_id != null)
                {
                    query += " and esd.place_of_posting_id = " + termination.place_of_posting_id;
                }
                if (termination.appointment_status_id != 0 && termination.appointment_status_id != null)
                {
                    query += " and esd.appointment_status_id = " + termination.appointment_status_id;
                }
                query += ") as emp Where emp.esetActive = 1 or emp.esetActive is null order by emp.esetUpdatedOn desc";
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
        public async Task<DataResponse> PostEmployeeExtensionTermination(Employee_ExtensionTermination_DTO employee)
        {
            //org sp emp_post_employee_service_extend_termination
            string query = null;
            var item = false;
            System.Data.SqlClient.SqlTransaction Trans = null;
            try
            {
                OpenContext();
                Trans = _connection._Connection.BeginTransaction(IsolationLevel.Serializable);
                _sqlCommand.Add_Transaction(Trans);



                string ServiceEffectiveTo = Convert.ToDateTime(employee.service_effective_to).ToString("yyyy-MM-dd");



                if (employee.employee_service_id != 0)
                {
                    //_sqlCommand.Add_Parameter_WithValue("prm_service_extend_terminate_id", employee.service_extend_terminate_id);
                    //_sqlCommand.Add_Parameter_WithValue("prm_employee_service_id", employee.employee_service_id);

                    query = $"Update emp_employee_service_details " +
                                $"SET " +
                                $"effective_to = '{ServiceEffectiveTo}' " +
                                $"where " +
                                $"employee_service_id = {employee.employee_service_id} ";


                    _sqlCommand.Execute_Query(query, CommandType.Text);

                    if (employee.service_extend_terminate_id != 0)
                    {
                        query += $" Update emp_employee_service_extend_termination " +
                                "SET " +
                                "is_active = 0 " +
                                "Where " +
                                $"service_extend_terminate_id = {employee.service_extend_terminate_id}";
                        _sqlCommand.Execute_Query(query, CommandType.Text);
                    }
                }

                _sqlCommand.Clear_CommandParameter();

                if (employee.employee_service_id != 0)
                {
                    _sqlCommand.Add_Parameter_WithValue("prm_service_extend_terminate_id", employee.service_extend_terminate_id);
                    _sqlCommand.Add_Parameter_WithValue("prm_employee_service_id", employee.employee_service_id);
                }
                _sqlCommand.Add_Parameter_WithValue("prm_employee_id", employee.employee_id);
                _sqlCommand.Add_Parameter_WithValue("prm_is_active", employee.is_active);
                _sqlCommand.Add_Parameter("prm_service_effective_to", SqlDbType.Date, ServiceEffectiveTo);
                _sqlCommand.Add_Parameter_WithValue("prm_remark", employee.remark);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", employee.created_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", employee.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", employee.updated_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", employee.updated_by);
                query += " Insert into emp_employee_service_extend_termination(employee_service_id, employee_id, is_active, service_effective_to, remark, created_on, created_by, updated_on, updated_by) " +
                        "values(@prm_employee_service_id, @prm_employee_id,  @prm_is_active, @prm_service_effective_to, @prm_remark, @prm_created_on, @prm_created_by, @prm_updated_on, @prm_updated_by)";

                item = await Task.Run(() => _sqlCommand.Execute_Query(query, CommandType.Text));

                Trans.Commit();

                if (item)
                {
                    return new DataResponse("Employee Service Extension/Termination Added Successfully", true);
                }
                else
                {
                    return new DataResponse("Failed To Add, Employee Service Extension/Termination", false);
                }
            }
            catch (Exception ex)
            {
                Trans.Rollback();
                // Handle the exception, log it, etc.
                return new DataResponse($"{ex.Message}, An error occurred", false);
                throw ex;
            }
            finally
            {
                CloseContext();
            }

        }


        public async Task<DataTable> GetEmployeeService_ExtensionTermination_Log(EmployeeExtension_Termination_DTO termination)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                var query = "select * from (select anu.UserName, anu.Email, " +
                    "epd.first_name, epd.middle_name, epd.last_name, esd.log_on, esd.effective_from, esd.effective_to   " +
                    ", esd.date_of_joining, esd.employee_service_id, esd.is_active, " +
                    "cm.company_id, cm.company_name, " +
                    "bm.branch_id, bm.branch_name, " +
                    "sm.state_id, sm.state_name, " +
                    "dm.district_id, dm.district_name, " +
                    "popm.place_of_posting_id, popm.place_of_posting_name, " +
                    "apm.appointment_status_id, apm.appointment_status_name, " +
                    "divm.division_id, divm.division_name, " +
                    "desm.designation_id, desm.designation_name, " +
                    "eset.service_extend_terminate_id,eset.is_active as esetActive, eset.updated_on as esetUpdatedOn, eset.service_effective_to, eset.remark " +
                    "from AspNetUsers as anu " +
                    "Join emp_employee_personal_details as epd on anu.UserName = epd.employee_id " +
                    "Join emp_employee_service_details_log as esd on  anu.UserName = esd.employee_id " +
                    "Join emp_company_master as cm on cm.company_id = esd.company_id " +
                    "Join emp_branch_master as bm on bm.branch_id = esd.branchoffice_id " +
                    "Join emp_state_master as sm on sm.state_id = esd.state_id " +
                    "Join emp_district_master as dm on dm.district_id = esd.district_id " +
                    "Join emp_place_of_posting_master as popm on popm.place_of_posting_id = esd.place_of_posting_id " +
                    "Join emp_appointment_status_master apm on apm.appointment_status_id = esd.appointment_status_id " +
                    "Join emp_division_master as divm on divm.division_id = esd.division_id " +
                    "Join emp_designation_master desm on desm.designation_id = esd.designation_id " +
                    "left Join emp_employee_service_extend_termination as eset on eset.employee_id = esd.employee_id and FORMAT(esd.log_on, 'yyyy-MM-dd HH:mm') = FORMAT(eset.updated_on, 'yyyy-MM-dd HH:mm') " +
                    "Where anu.UserStatus = 1 ";

                if (termination.company_id != 0 && termination.company_id != null)
                {
                    query += " and esd.company_id = " + termination.company_id;
                }
                if (termination.branch_id != 0 && termination.branch_id != null)
                {
                    query += " and esd.branchoffice_id = " + termination.branch_id;
                }
                if (termination.state_id != 0 && termination.state_id != null)
                {
                    query += " and esd.state_id = " + termination.state_id;
                }
                if (termination.district_id != 0 && termination.district_id != null)
                {
                    query += " and esd.district_id = " + termination.district_id;
                }
                if (termination.place_of_posting_id != 0 && termination.place_of_posting_id != null)
                {
                    query += " and esd.place_of_posting_id = " + termination.place_of_posting_id;
                }
                if (termination.appointment_status_id != 0 && termination.appointment_status_id != null)
                {
                    query += " and esd.appointment_status_id = " + termination.appointment_status_id;
                }
                query += ") as emp order by emp.esetUpdatedOn desc";
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



    }
}
