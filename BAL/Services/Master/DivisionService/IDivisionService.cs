using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.DivisionService
{
    public interface IDivisionService
    {
        Task<DataResponse> PostDivision(Division_DTO division);
        Task<DataResponse> PutDivision(Division_DTO division);
        Task<List<Division_DTO>> GetDivision();
        Task<DataResponse> DeleteDivision(long division);
    }
}
