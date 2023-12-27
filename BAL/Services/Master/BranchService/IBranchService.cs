using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

// =============================================
// -- Author:		Mukesh Shah
// -- Create date: 09-Nov-2023
// =============================================

namespace BAL.Services.Master.BranchOfficeService
{
    public interface IBranchService
    {
        Task<DataResponse> PostBranch(Branch_DTO branchOffice);
        Task<DataResponse> PutBranch(Branch_DTO branchOffice);
        Task<DataResponse> DeleteBranch(long id);
        Task<List<Branch_DTO>> GetBranch();

    }
}
