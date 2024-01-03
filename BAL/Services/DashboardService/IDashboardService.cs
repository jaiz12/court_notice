using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Dashboard
{
    public interface IDashboardService
    {
        Task<DataTable> GetTotalEmployeeWithCompany();
    }
}
