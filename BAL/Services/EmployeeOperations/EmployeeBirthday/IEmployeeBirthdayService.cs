using DTO.Models;
using DTO.Models.BirthdayWishes;
using DTO.Models.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.EmployeeOperations.EmployeeBirthday
{
    public interface IEmployeeBirthdayService
    {
        Task<List<EmployeeBirthday_DTO>> GetBirthday();
        Task<DataTable> GetBirthdayComment(string employee_id);
        Task<DataResponse> Post(EmployeeBirthday_DTO data);
        Task<DataResponse> Delete(long data);
        Task<List<BirthdayListForRealTimeDTO>> GetBirthdayListForRealTimeChat();
    }
}
