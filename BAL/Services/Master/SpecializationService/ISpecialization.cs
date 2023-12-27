using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.SpecializationService
{
    public interface ISpecialization
    {
        Task<DataResponse> Post(Specialization_DTO data);
        Task<List<Specialization_DTO>> GetSpecialization();
        Task<DataResponse> Update(Specialization_DTO data);
        Task<DataResponse> Delete(long data);
    }
}
