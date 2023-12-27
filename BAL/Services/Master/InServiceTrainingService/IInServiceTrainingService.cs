using System.Data;
using System.Threading.Tasks;

namespace DTO.Models.Master
{
    public interface IInServiceTrainingService
    {
        Task<DataTable> GetInServiceTraining();
        DataResponse AddInServiceTraining(InServiceTraining_DTO model);
        DataResponse EditInServiceTraining(InServiceTraining_DTO model);
        Task<DataResponse> DeleteInServiceTraining(long id);
    }
}
