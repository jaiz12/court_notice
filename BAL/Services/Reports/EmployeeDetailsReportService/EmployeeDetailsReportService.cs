using Common.DbContext;
using DTO.Models.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BAL.Services.Reports.EmployeeDetailsReportService
{
    public class EmployeeDetailsReportService : MyDbContext, IEmployeeDetailsReportService
    {
        public async Task<DataTable> GetEmployeeDetailsReport(FilterReports data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                var query = $"Select t1.employee_id as Emp_Code , t1.first_name as First_Name, t1.middle_name as Middle_Name, t1.last_name as Last_Name, t1.date_of_birth as Date_Of_Birth, t3.gender_name as Gender,t4.company_name as Company, t5.branch_name as Branch, t6.designation_name as Designation, t7.division_name as Division,  t8.appointment_status_name as Service_Status, t2.date_of_joining as Date_Of_Joining, t9.place_of_posting_name as Place_Of_Posting, t10.pf_uan_no as UAN_No, t10.pf_bank_account_no as Bank_Account_No   from emp_employee_personal_details as t1  join emp_employee_service_details as t2 on t1.employee_id = t2.employee_id   join emp_gender_master as t3 on t1.gender_id = t3.gender_id  join emp_company_master as t4 on t2.company_id = t4.company_id  join emp_branch_master as t5 on t2.branchoffice_id = t5.branch_id  join emp_designation_master as t6 on t2.designation_id = t6.designation_id  join emp_division_master as t7 on t2.division_id = t7.division_id  join emp_appointment_status_master as t8 on t2.appointment_status_id = t8.appointment_status_id  join emp_place_of_posting_master as t9 on t2.place_of_posting_id = t9.place_of_posting_id left join emp_employee_other_details as t10 on t1.employee_id = t10.employee_id join (select employee_id , MAX(updated_on) as updated_on from emp_employee_service_details group by employee_id) t11 on t2.employee_id = t11.employee_id and t11.updated_on = t2.updated_on where t1.employee_id = t2.employee_id";                
                
                if (data.company_id != 0)
                {
                    query += $" and t2.company_id = {data.company_id}";
                }
                if (data.branch_id != 0)
                {
                    query += $" and t2.branchoffice_id = {data.branch_id}";
                }
                if (data.division_id != 0)
                {
                    query += $" and t2.division_id = {data.division_id}";
                }
                if (data.state_id != 0)
                {
                    query += $" and t2.state_id = {data.state_id}";
                }
                if (data.district_id != 0)
                {
                    query += $" and t2.district_id = {data.district_id}";
                }
                if (data.place_of_posting_id != 0)
                {
                    query += $" and t2.place_of_posting_id = {data.place_of_posting_id}";
                }
                if(data.appointment_status_id  != 0)
                {
                    query += $" and t2.appointment_status_id = {data.appointment_status_id}";
                }
                if (data.active != "")
                {
                    query += $" and t2.is_active = {data.active}";
                }
                query += ";";
                var result = await Task.Run(() => _sqlCommand.Select_Table(query, CommandType.Text));
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
        public async Task<DataTable> GetEmployeeServiceHistoryReport(FilterReports data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                var query = $"Select t1.employee_id as Emp_Code,t1.first_name as first_Name,t1.middle_name as middile_Name,t1.last_name as last_Name,t2.effective_to as service_end_date,t4.company_name as company,t5.branch_name as branch,t6.designation_name as designation,t7.division_name as division,t8.appointment_status_name as appointment_status, t2.date_of_joining, t9.place_of_posting_name as place_of_posting from emp_employee_personal_details as t1 join emp_employee_service_details as t2 on t1.employee_id = t2.employee_id join emp_gender_master as t3 on t1.gender_id = t3.gender_id join emp_company_master as t4 on t2.company_id = t4.company_id join emp_branch_master as t5 on t2.branchoffice_id = t5.branch_id join emp_designation_master as t6 on t2.designation_id = t6.designation_id join emp_division_master as t7 on t2.division_id = t7.division_id join emp_appointment_status_master as t8 on t2.appointment_status_id = t8.appointment_status_id join emp_place_of_posting_master as t9 on t2.place_of_posting_id = t9.place_of_posting_id join (select employee_id , MAX(updated_on) as updated_on from emp_employee_service_details group by employee_id) t10 on t2.employee_id = t10.employee_id and t10.updated_on = t2.updated_on where t1.employee_id is not null";                
                if (data.company_id != 0)
                {
                    query += $" and t2.company_id = {data.company_id}";
                }
                if (data.branch_id != 0)
                {
                    query += $" and t2.branchoffice_id = {data.branch_id}";
                }
                if (data.division_id != 0)
                {
                    query += $" and t2.division_id = {data.division_id}";
                }
                if (data.state_id != 0)
                {
                    query += $" and t2.state_id = {data.state_id}";
                }
                if (data.district_id != 0)
                {
                    query += $" and t2.district_id = {data.district_id}";
                }
                if (data.place_of_posting_id != 0)
                {
                    query += $" and t2.place_of_posting_id = {data.place_of_posting_id}";
                }
                if(data.appointment_status_id  != 0)
                {
                    query += $" and t2.appointment_status_id = {data.appointment_status_id}";
                }
                if (data.active != "")
                {
                    query += $" and t2.is_active = {data.active}";
                }
                query += ";";
                var result = await Task.Run(() => _sqlCommand.Select_Table(query, CommandType.Text));
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
        public async Task<DataTable> GetEmployeeLeaveHistoryReport(EmpLeaveHistoryFilter data)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                var query =
                    $"SELECT t1.employee_id, t1.leave_type_id, t12.leave_type_name,  t1.financial_year_id, t1.total_leave, ISNULL(t2.total_leave_applied, 0) as total_leave_applied, " +
                    $"t3.first_name, t3.middle_name, t3.last_name, t5.company_name,  t6.branch_name,  t7.place_of_posting_name, t8.division_name, t9.designation_name, t10.appointment_status_name " +
                    $"FROM lv_employee_wise_leave_config as t1 " +
                    $"JOIN (SELECT employee_id, leave_type_id, SUM(total_leave_applied) as total_leave_applied " +
                    //note for dev
                    //now in this query, show leave apply cases
                    //if user want both type of data for ex, applyleave_case & remainleave_case just change above join to left join.
                    $"FROM  lv_apply_leave ";
                if (data.month != 0)
                {
                    query += $" where MONTH(updated_on) = {data.month} ";
                }
                query += $"GROUP BY leave_type_id, employee_id ) as t2  ON t2.leave_type_id = t1.leave_type_id and t2.employee_id = t1.employee_id " +
                    $"JOIN emp_employee_personal_details as t3 ON t3.employee_id = t1.employee_id " +
                    $"Join emp_employee_service_details as t4 ON t4.employee_id = t1.employee_id " +
                    $"Join emp_company_master as t5 On t5.company_id = t4.company_id " +
                    $"Join emp_branch_master as t6 ON t6.branch_id = t4.branchoffice_id " +
                    $"Join emp_place_of_posting_master as t7 On t7.place_of_posting_id = t4.place_of_posting_id " +
                    $"Join emp_division_master as t8 ON t8.division_id = t4.division_id " +
                    $"Join emp_designation_master as t9 on t9.designation_id = t4.designation_id " +
                    $"Join emp_appointment_status_master as t10 On t10.appointment_status_id = t4.appointment_status_id " +
                    $"Join lv_financial_year_master as t11 ON t11.financial_year_id = t1.financial_year_id " +
                    $"Join lv_leave_type_master as t12 on t12.leave_type_id = t1.leave_type_id " +
                    $"WHERE t4.is_active = 1 And GETDATE() between t11.from_date and t11.to_date ";

                if(data.company_id != 0)
                {
                    query += $" and t4.company_id = {data.company_id} ";
                }
                if(data.branch_id != 0)
                {
                    query += $" and t4.branchoffice_id = {data.branch_id} ";
                }
                if(data.place_of_posting_id != 0)
                {
                    query += $" and t4.place_of_posting_id = {data.place_of_posting_id} ";
                }
                if(data.division_id != 0)
                {
                    query += $" and t4.division_id = {data.division_id} ";
                }
                if(data.designation_id != 0)
                {
                    query += $" and t4.designation_id = {data.designation_id} ";
                }
                if(data.appointment_status_id != 0)
                {
                    query += $" and t4.appointment_status_id = {data.appointment_status_id} ";
                }
                if(data.leave_type_id != 0)
                {
                    query += $" and t1.leave_type_id = {data.leave_type_id} ";
                }
                
                query += " order by t12.leave_type_name desc;";
                var result = await Task.Run(() => _sqlCommand.Select_Table(query, CommandType.Text));
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
            finally
            {
                CloseContext();
            }
        }
        public async Task<DataTable> checkUserRoleManager(string UserId, string company_name, string Name)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();

                string query = "select eSD.division_id from UserCompanyRoleMapping as uCRM " +
                                "JOIN AspNetUsers as aNU on uCRM.UserId = aNU.Id " +
                                "JOIN emp_employee_service_details as eSD on aNU.UserName = eSD.employee_id " +
                                $"Where uCRM.UserId = '{UserId}' and uCRM.company_name = '{company_name}' and uCRM.Name IN ({Name}); ";

                DataTable getUserRoleManagerDivisionIdDT = await Task.Run(() => _sqlCommand.Select_Table(query, CommandType.Text));
                return getUserRoleManagerDivisionIdDT;
            }
            catch(Exception ex)
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
