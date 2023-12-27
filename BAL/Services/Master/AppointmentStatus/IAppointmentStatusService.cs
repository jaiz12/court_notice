using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.MasterServices.AppointmentStatus
{
    public interface IAppointmentStatusService
    {
        List<AppointmentStatusDTO> Get();
        public DataResponse Post(AppointmentStatusDTO item);
        public DataResponse Update(AppointmentStatusDTO item);
        public Task<DataResponse> Delete(long id);
    }
}
