using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.MaritalStatusService
{
    public interface IMaritalStatus
    {
        Task<DataResponse> Post(MaritalStatus_DTO data);
        Task<List<MaritalStatus_DTO>> GetMaritalStatus();
        Task<DataResponse> Update(MaritalStatus_DTO data);
        Task<DataResponse> Delete(long data);
    }
}
