using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.MasterServices.Qualification
{
    public interface IQualificationService
    {
        public List<QualificationDTO> Get();
        public DataResponse Post(QualificationDTO item);
        public DataResponse Update(QualificationDTO item);
        public Task<DataResponse> Delete(long id);
    }
}
