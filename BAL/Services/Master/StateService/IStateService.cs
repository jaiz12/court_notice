using DTO.Models;
using DTO.Models.Master;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.State
{
    public interface IStateService
    {
        Task<DataTable> GetState();
        DataResponse AddState(State_DTO model);
        DataResponse EditState(State_DTO model);
        Task<DataResponse> DeleteState(long id);
    }
}
