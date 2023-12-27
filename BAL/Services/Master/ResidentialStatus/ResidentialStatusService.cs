using BAL.Services.Master.Common;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.MasterServices.ResidentialStatus
{
    internal class ResidentialStatusService : IResidentialStatusService
    {
        private IMasterCommonService _commonService;
        public ResidentialStatusService(IMasterCommonService commonService)
        {
            _commonService = commonService;
        }

        public List<ResidentialStatusDTO> Get()
        {
            DataTable dt = _commonService.Get("emp_get_residential_status_master");
            return DataTableVsListOfType.ConvertDataTableToList<ResidentialStatusDTO>(dt);
        }

        public DataResponse Post(ResidentialStatusDTO item)
        {
            return _commonService.PostOrUpdate("emp_post_residential_status_master", "residential_status", item, false);
        }

        public DataResponse Update(ResidentialStatusDTO item)
        {
            return _commonService.PostOrUpdate("emp_update_residential_status_master", "residential_status", item, true);
        }

        public async Task<DataResponse> Delete(long id)
        {
            return await _commonService.Delete("emp_delete_residential_status_master", "residential_status", id);
        }
    }
}
