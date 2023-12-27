using BAL.Services.Master.Common;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.MasterServices.AppointmentStatus
{
    internal class AppointmentStatusService : IAppointmentStatusService
    {

        private IMasterCommonService _commonService;
        public AppointmentStatusService(IMasterCommonService commonService)
        {
            _commonService = commonService;
        }

        public List<AppointmentStatusDTO> Get()
        {
            DataTable dt = _commonService.Get("emp_get_appointment_status_master");
            return DataTableVsListOfType.ConvertDataTableToList<AppointmentStatusDTO>(dt);
        }

        public DataResponse Post(AppointmentStatusDTO item)
        {
            return _commonService.PostOrUpdate("emp_post_appointment_status_master", "appointment_status", item, false);
        }

        public DataResponse Update(AppointmentStatusDTO item)
        {
            return _commonService.PostOrUpdate("emp_update_appointment_status_master", "appointment_status", item, true);
        }

        public async Task<DataResponse> Delete(long id)
        {
            return await _commonService.Delete("emp_delete_appointment_status_master", "appointment_status", id);
        }

    }
}
