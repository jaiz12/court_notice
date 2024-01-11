using DTO.Models.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Reports.EmployeeDetailsReportService
{
    public interface IEmployeeDetailsReportService
    {
        Task<DataTable> GetEmployeeDetailsReport(FilterReports data);
        Task<DataTable> GetEmployeeServiceHistoryReport(FilterReports data);
        Task<DataTable> GetEmployeeLeaveHistoryReport(EmpLeaveHistoryFilter data);
        Task<DataTable> checkUserRoleManager(string userId, string company_name, string Name);
    }
}
