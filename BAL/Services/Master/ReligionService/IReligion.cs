using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.ReligionService
{
    public interface IReligion
    {
        Task<List<Religion_DTO>> GetReligion();
        Task<DataResponse> Post(Religion_DTO data);
        Task<DataResponse> Update(Religion_DTO data);
        Task<DataResponse> Delete(long data);
    }
}
