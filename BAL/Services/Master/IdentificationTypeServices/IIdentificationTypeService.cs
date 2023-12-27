using DTO.Models;
using DTO.Models.Master;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.IdentificationTypeServices
{
    public interface IIdentificationTypeService
    {
        Task<DataTable> GetIdentificationType();
        DataResponse AddIdentificationType(IdentificationType_DTO model);
        DataResponse EditIdentificationType(IdentificationType_DTO model);
        Task<DataResponse> DeleteIdentificationType(long id);
    }
}
