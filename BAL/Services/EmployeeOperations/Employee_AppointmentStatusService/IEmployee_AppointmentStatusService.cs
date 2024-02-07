using DTO.Models;
using DTO.Models.EmployeeOperation;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.EmployeeOperations.Employee_AppointmentStatusService
{
    public interface IEmployee_AppointmentStatusService
    {
        Task<DataTable> GetEmployee_AppointmentStatus(EmployeePromotionOptionValues_DTO promotionOptionValues);
        Task<DataResponse> PromoteEmployeeDesignation(PromotedEmployeeService_DTO promote);
        Task<DataTable> GetEmployeePromotionHistory(EmployeePromotionOptionValues_DTO promotionOptionValues);
        Task<DataResponse> PutPromotedEmployeeDetails(EditPromotedEmployee_DTO dataModel);
        Task<DataResponse> DeletePromotedEmployeeHistoryDetails(long employee_service_id);
    }
}
