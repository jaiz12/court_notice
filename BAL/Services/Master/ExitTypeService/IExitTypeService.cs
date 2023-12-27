using DTO.Models;
using DTO.Models.Master;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.ExitTypeService
{
    public interface IExitTypeService
    {
        Task<DataTable> GetExitType();
        DataResponse AddExitType(ExitType_DTO model);
        DataResponse EditExitType(ExitType_DTO model);
        Task<DataResponse> DeleteExitType(long id);
    }
}
