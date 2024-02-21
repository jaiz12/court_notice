using DTO.Models;
using DTO.Models.EmployeeOperation;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.EmployeeOperations.EmployeeExtensionService
{
    public interface IEmployeeExtension_TerminationService
    {
        Task<DataTable> GetEmployeeServiceForExtensionTermination(EmployeeExtension_Termination_DTO termination);
        Task<DataResponse> PostEmployeeExtensionTermination(Employee_ExtensionTermination_DTO employee);
        Task<DataTable> GetEmployeeService_ExtensionTermination_Log(EmployeeExtension_Termination_DTO termination);
    }
}
