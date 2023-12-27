using DTO.Models;
using DTO.Models.Master;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.DesignationService
{
    public interface IDesignationService
    {
        Task<DataTable> GetDesignations();
        DataResponse AddDesignation(Designation_DTO model);
        DataResponse EditDesignation(Designation_DTO model);
        Task<DataResponse> DeleteDesignation(long id);
    }
}
