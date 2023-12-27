using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.NationalityService
{
    public interface INationalityService
    {
        Task<DataResponse> PostNationality(Nationality_DTO nationality);
        Task<DataResponse> PutNationality(Nationality_DTO nationality);
        Task<List<Nationality_DTO>> GetNationality();
        Task<DataResponse> DeleteNationality(long id);
    }
}
