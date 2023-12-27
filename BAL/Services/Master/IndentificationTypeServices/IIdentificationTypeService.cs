using DTO.Models;
using DTO.Models.Master;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.IndentificationTypeServices
{
    public interface IIdentificationTypeService
    {
        Task<DataTable> GetIndentificationType();
        DataResponse AddIndentificationType(IdentificationType_DTO model);
        DataResponse EditIndentificationType(IdentificationType_DTO model);
        Task<DataResponse> DeleteIndentificationType(long id);
    }
}
