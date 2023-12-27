using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.BoardsService
{
    public interface IBoardsService
    {
        Task<List<Boards_DTO>> GetBoards();
        Task<DataResponse> Post(Boards_DTO data);
        Task<DataResponse> Update(Boards_DTO data);
        Task<DataResponse> Delete(long data);
    }
}
