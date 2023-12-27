using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.MasterServices.Community
{
    public interface ICommunityService
    {
        List<CommunityDTO> Get();
        public DataResponse Post(CommunityDTO item);
        public DataResponse Update(CommunityDTO item);
        public Task<DataResponse> Delete(long id);
    }
}
