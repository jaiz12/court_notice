using DTO.Models;
using DTO.Models.EmployeeOperation;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.EmployeeOperations.EmployeeAwardService
{
    public interface IEmployeeAwardService
    {

        Task<DataTable> GetEmployeeBasicServiceForAward(string username);
        Task<DataResponse> PostEmployeeAward(EmployeeAward_DTO employeeAward);
        Task<List<EmployeeAward_DTO>> GetEmployeeAward(EmpAwardFilter_DTO filters);
        Task<DataResponse> PutEmployeeAward(EmployeeAwardEdit_DTO employeeAward);
        Task<DataResponse> DeleteEmployeeAward(long employee_award_id);
        Task<DataTable> GetEmployeeDetailsForAward(EmployeeAwardFilter_DTO filters);
    }
}
