using DTO.Models;
using DTO.Models.Master;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.DistrictService
{
    public interface IDistrictService
    {
        Task<DataTable> GetDistrict();
        Task<DataResponse> AddDistrict(District_DTO model);
        Task<DataResponse> EditDistrict(District_DTO model);
        Task<DataResponse> DeleteDistrict(long id);
    }
}
