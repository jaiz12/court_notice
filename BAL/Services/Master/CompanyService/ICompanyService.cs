using DTO.Models;
using DTO.Models.Master;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.CompanyService
{
    public interface ICompanyService
    {
        Task<DataTable> GetCompany();
        DataResponse AddCompany(Company_DTO model);
        DataResponse EditCompany(Company_DTO model);
        Task<DataResponse> DeleteCompany(long id);
    }
}
