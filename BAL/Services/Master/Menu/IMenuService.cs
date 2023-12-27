using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.Menu
{
    public interface IMenuService
    {
        Task<DataResponse> PostMenu(Menu_DTO menu);
        Task<DataResponse> Update(Menu_DTO menu);
        Task<List<Menu_DTO>> GetMenus();
        Task<DataResponse> Delete(long data);
    }
}
