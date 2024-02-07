using BAL.Services.EmployeeOperations.Employee_AppointmentStatusService;
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

namespace BAL.Services.EmployeeOperations.Employee_AppointmentStatusService
{
    public class Employee_AppointmentStatusService : MyDbContext, IEmployee_AppointmentStatusService
    {
        public async Task<DataTable> GetEmployee_AppointmentStatus(Employee_AppointmentStatus_DTO model)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_company_id", model.company_id);
                _sqlCommand.Add_Parameter_WithValue("prm_branch_id", model.branch_id);
                _sqlCommand.Add_Parameter_WithValue("prm_state_id", model.state_id);
                _sqlCommand.Add_Parameter_WithValue("prm_district_id", model.district_id);
                _sqlCommand.Add_Parameter_WithValue("prm_place_of_posting_id", model.place_of_posting_id);
                var result = await Task.Run(() => _sqlCommand.Select_Table("emp_get_employee_appointment_status", CommandType.StoredProcedure));
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
        
        public async Task<DataResponse> UpdateEmployee_AppointmentStatus(Employee_AppointmentStatus_DTO_Edit model)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_employee_id", model.employee_id);
                _sqlCommand.Add_Parameter_WithValue("prm_appointment_status_id", model.appointment_status_id);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", model.updated_by);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_employee_appointment_status", CommandType.StoredProcedure));
                if (result)
                    return new DataResponse("'Appointment Status' Updated Successfully", true);
                else
                    return new DataResponse("'Appointment Status'' Already Exists", false);
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
        public async Task<DataTable> GetEmployee_AppointmentStatusHistory(Employee_AppointmentStatus_DTO model)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_company_id", model.company_id);
                _sqlCommand.Add_Parameter_WithValue("prm_branch_id", model.branch_id);
                _sqlCommand.Add_Parameter_WithValue("prm_state_id", model.state_id);
                _sqlCommand.Add_Parameter_WithValue("prm_district_id", model.district_id);
                _sqlCommand.Add_Parameter_WithValue("prm_place_of_posting_id", model.place_of_posting_id);
                var result = await Task.Run(() => _sqlCommand.Select_Table("emp_get_employee_appointment_status_history", CommandType.StoredProcedure));
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
       
    }
}
