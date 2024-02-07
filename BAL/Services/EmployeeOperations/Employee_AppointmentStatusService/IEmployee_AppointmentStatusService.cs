using DTO.Models;
using DTO.Models.EmployeeOperation;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.EmployeeOperations.Employee_AppointmentStatusService
{
    public interface IEmployee_AppointmentStatusService
    {
        Task<DataTable> GetEmployee_AppointmentStatus(Employee_AppointmentStatus_DTO model);
        Task<DataResponse> UpdateEmployee_AppointmentStatus(Employee_AppointmentStatus_DTO_Edit model);
        Task<DataTable> GetEmployee_AppointmentStatusHistory(Employee_AppointmentStatus_DTO model);
    }
}
