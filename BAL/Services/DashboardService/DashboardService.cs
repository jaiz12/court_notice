using Common.DbContext;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Dashboard
{
    public class DashboardService : MyDbContext, IDashboardService
    {
        /// Author : Sandeep Chauhan
        /// Date : 28-12-2023
        public async Task<DataTable> GetTotalEmployeeWithCompany()
        {
            try
            {
                OpenContext();
                var result = await Task.Run(() => _sqlCommand.Select_Table("emp_get_total_employee_with_company", CommandType.StoredProcedure));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }
    }
}
