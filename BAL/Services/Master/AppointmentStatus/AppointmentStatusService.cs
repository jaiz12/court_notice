using BAL.Services.Master.Common;
using Common.DbContext;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.MasterServices.AppointmentStatus
{
    public class AppointmentStatusService: MyDbContext, IAppointmentStatusService
    {

        //private IMasterCommonService _commonService;
        //public AppointmentStatusService(IMasterCommonService commonService)
        //{
        //    _commonService = commonService;
        //}

        //public List<AppointmentStatusDTO> Get()
        //{
        //    DataTable dt = _commonService.Get("emp_get_appointment_status_master");
        //    return DataTableVsListOfType.ConvertDataTableToList<AppointmentStatusDTO>(dt);
        //}

        //public DataResponse Post(AppointmentStatusDTO item)
        //{
        //    return _commonService.PostOrUpdate("emp_post_appointment_status_master", "appointment_status", item, false);
        //}

        //public DataResponse Update(AppointmentStatusDTO item)
        //{
        //    return _commonService.PostOrUpdate("emp_update_appointment_status_master", "appointment_status", item, true);
        //}

        //public async Task<DataResponse> Delete(long id)
        //{
        //    return await _commonService.Delete("emp_delete_appointment_status_master", "appointment_status", id);
        //}

        public async Task<List<AppointmentStatusDTO>> Get()
        {
            try
            {
                OpenContext();
                DataTable appointmentStatusDT = _sqlCommand.Select_Table("emp_get_appointment_status_master", CommandType.StoredProcedure);
                return DataTableVsListOfType.ConvertDataTableToList<AppointmentStatusDTO>(appointmentStatusDT);
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed To Fetch, {ex.Message}");
            }
            finally
            {
                CloseContext();
            }
        }

        public async Task<DataResponse> Post(AppointmentStatusDTO item)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_appointment_status_name", item.appointment_status_name);
                _sqlCommand.Add_Parameter_WithValue("prm_created_by", item.created_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", item.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_created_on", item.created_on = DateTime.Now);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", item.updated_on = DateTime.Now);

                var res = await Task.Run(() => _sqlCommand.Execute_Query("emp_post_appointment_status_master", CommandType.StoredProcedure));

                if (res)
                    return new DataResponse("Appointment Status Added Successfully", true);
                else
                    return new DataResponse("Appointment Status Already Exists", false);
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

        public async Task<DataResponse> Update(AppointmentStatusDTO item)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_appointment_status_id", item.appointment_status_id);
                _sqlCommand.Add_Parameter_WithValue("prm_appointment_status_name", item.appointment_status_name);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_by", item.updated_by);
                _sqlCommand.Add_Parameter_WithValue("prm_updated_on", item.updated_on = DateTime.Now);

                var res = await Task.Run(() => _sqlCommand.Execute_Query("emp_update_appointment_status_master", CommandType.StoredProcedure));

                if (res)
                    return new DataResponse("Appointment Status Updated Successfully", true);
                else
                    return new DataResponse("Appointment Status Already Exists", false);
            }
            catch (Exception ex)
            {
                return new DataResponse(ex.Message, false);
            }
            finally
            {
                CloseContext();
            }
        }

        public async Task<DataResponse> Delete(long appointment_status_id)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                _sqlCommand.Add_Parameter_WithValue("prm_appointment_status_id", appointment_status_id);
                var result = await Task.Run(() => _sqlCommand.Execute_Query("emp_delete_appointment_status_master", CommandType.StoredProcedure));

                if (result)
                    return new DataResponse("Appointment Status Deleted Successfully", true);
                else
                    return new DataResponse("Failed To Delete Appointment Status", false);
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
