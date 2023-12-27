using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.BloodGroupService
{
    public interface IBloodGroupService
    {
        Task<DataResponse> PostBloodGroup(BloodGroup_DTO bloodGroupList);
        Task<DataResponse> PutBloodGroup(BloodGroup_DTO bloodGroupList);
        Task<List<BloodGroup_DTO>> GetBloodGroup();
        Task<DataResponse> DeleteBloodGroup(long id);
    }
}
