using DTO.Models;
using DTO.Models.ContentManagementSystem;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.ContentManagementSystem.ContentMessageManagementSystem
{
    public interface IContentMessageManagementSystemService
    {
        Task<DataResponse> PostCmsMessage(ContentMessageManagementSystem_DTO message);
        Task<DataResponse> PutCmsMessage(ContentMessageManagementSystem_DTO message);
        Task<DataResponse> DeleteCmsMessage(long message_id);
        Task<DataTable> GetCmsMessage();
        Task<DataTable> GetCmsMessageForLogin();

        Task<DataTable> GetCmsMessageCount();
    }
}
