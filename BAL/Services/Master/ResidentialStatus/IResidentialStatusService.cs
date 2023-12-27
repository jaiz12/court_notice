using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.MasterServices.ResidentialStatus
{
    public interface IResidentialStatusService
    {
        List<ResidentialStatusDTO> Get();
        public DataResponse Post(ResidentialStatusDTO item);
        public DataResponse Update(ResidentialStatusDTO item);
        public Task<DataResponse> Delete(long id);
    }
}
