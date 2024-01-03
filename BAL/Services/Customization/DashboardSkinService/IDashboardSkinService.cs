using DTO.Models;
using DTO.Models.Customization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Customization.DashboardSkinService
{
    public interface IDashboardSkinService
    {
        Task<DataResponse> PostOrUpdateDashboardSkin(List<DashboardSkin_DTO> _dashboardSkin);
        Task<DataResponse> PutDashboardSkin(List<DashboardSkin_DTO> _dashboardSkin);
        Task<List<DashboardSkin_DTO>> GetDashboardSkin();
    }
}
