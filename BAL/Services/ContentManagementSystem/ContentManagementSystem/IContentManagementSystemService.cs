using DTO.Models;
using DTO.Models.ContentManagementSystem;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.ContentManagementSystem.ContentManagementSystem
{
    public interface IContentManagementSystemService
    {
        Task<DataResponse> PostCMSDetails(ContentManagementSystem_DTO cmsDetails);
        Task<DataResponse> PutCMSDetails(ContentManagementSystem_DTO cmsDetails);
        Task<DataResponse> DeleteCMSDetails(long cms_id);
        Task<DataTable> GetCMSDetails();
        Task<DataTable> GetCMSDetailsOnLoad();
        Task<DataTable> GetCmsNoticeCount();
    }
}
