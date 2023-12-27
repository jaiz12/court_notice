using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.ModeOfTrainingService
{
    public interface IModeOfTraining
    {
        Task<List<Mode_of_training_DTO>> GetModeOfTraining();
        Task<DataResponse> Post(Mode_of_training_DTO data);
        Task<DataResponse> Update(Mode_of_training_DTO data);
        Task<DataResponse> Delete(long data);
    }
}
