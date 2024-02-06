using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Dashboard
{
    public interface IDashboardService
    {
        Task<DataTable> GetTotalEmployeeWithCompany();
    }
}
