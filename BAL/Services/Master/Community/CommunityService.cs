using BAL.Services.Master.Common;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.MasterServices.Community
{
    internal class CommunityService : ICommunityService
    {

        private IMasterCommonService _commonService;
        public CommunityService(IMasterCommonService commonService)
        {
            _commonService = commonService;
        }

        public List<CommunityDTO> Get()
        {
            DataTable dt = _commonService.Get("emp_get_community_master");
            return DataTableVsListOfType.ConvertDataTableToList<CommunityDTO>(dt);
        }
        public DataResponse Post(CommunityDTO item)
        {
            return _commonService.PostOrUpdate("emp_post_community_master", "community", item, false);
        }

        public DataResponse Update(CommunityDTO item)
        {
            return _commonService.PostOrUpdate("emp_update_community_master", "community", item, true);
        }

        public async Task<DataResponse> Delete(long id)
        {
            return await _commonService.Delete("emp_delete_community_master", "community", id);
        }

    }
}
