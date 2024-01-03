using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.MasterServices.AppointmentStatus
{
    public interface IAppointmentStatusService
    {
        public Task<List<AppointmentStatusDTO>> Get();
        public Task<DataResponse> Post(AppointmentStatusDTO item);
        public Task<DataResponse> Update(AppointmentStatusDTO item);
        public Task<DataResponse> Delete(long id);
    }
}
