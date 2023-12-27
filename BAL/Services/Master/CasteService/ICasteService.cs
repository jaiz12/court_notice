using DTO.Models;
using DTO.Models.Master;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.CasteService
{
    public interface ICasteService
    {
        Task<DataTable> GetCaste();
        DataResponse AddCaste(Caste_DTO model);
        DataResponse EditCaste(Caste_DTO model);
        Task<DataResponse> DeleteCaste(long id);

    }
}
