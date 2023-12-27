using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.GenderService
{
    public interface IGenderService
    {
        Task<DataResponse> PostGender(Gender_DTO gender);
        Task<DataResponse> PutGender(Gender_DTO gender);
        Task<List<Gender_DTO>> GetGender();
        Task<DataResponse> DeleteGender(long id);
    }
}
