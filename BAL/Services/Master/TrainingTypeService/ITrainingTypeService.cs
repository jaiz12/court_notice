using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.TrainingTypeService
{
    public interface ITrainingTypeService
    {
        Task<DataResponse> PostTrainingType(TrainingType_DTO trainingType);
        Task<DataResponse> PutTrainingType(TrainingType_DTO trainingType);
        Task<List<TrainingType_DTO>> GetTrainingType();
        Task<DataResponse> DeleteTrainingType(long id);
    }
}
