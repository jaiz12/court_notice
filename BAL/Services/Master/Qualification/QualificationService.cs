using BAL.Services.Master.Common;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.MasterServices.Qualification
{
    internal class QualificationService : IQualificationService
    {
        private IMasterCommonService _commonService;
        public QualificationService(IMasterCommonService commonService)
        {
            _commonService = commonService;
        }

        public List<QualificationDTO> Get()
        {
            DataTable dt = _commonService.Get("emp_get_qualification_master");
            return DataTableVsListOfType.ConvertDataTableToList<QualificationDTO>(dt);
        }

        public DataResponse Post(QualificationDTO item)
        {
            return _commonService.PostOrUpdate("emp_post_qualification_master", "qualification", item, false);
        }

        public DataResponse Update(QualificationDTO item)
        {
            return _commonService.PostOrUpdate("emp_update_qualification_master", "qualification", item, true);
        }

        public async Task<DataResponse> Delete(long id)
        {
            return await _commonService.Delete("emp_delete_qualification_master", "qualification", id);
        }

    }
}
