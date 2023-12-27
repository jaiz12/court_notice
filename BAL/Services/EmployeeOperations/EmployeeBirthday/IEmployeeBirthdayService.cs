using DTO.Models.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.EmployeeOperations.EmployeeBirthday
{
    public interface IEmployeeBirthdayService
    {
        Task<List<EmployeeBirthday_DTO>> GetBirthday();
    }
}
