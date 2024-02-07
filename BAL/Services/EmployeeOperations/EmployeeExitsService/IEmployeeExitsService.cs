using DTO.Models;
using DTO.Models.EmployeeOperation.Exits;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.EmployeeOperations.EmployeeExitsService
{
    public interface IEmployeeExitsService
    {
        Task<DataTable> GetEmployeeBasicDetailsForExits(EmployeeExitsOptionValue_DTO _optionValue);
        Task<DataResponse> PutEmployeeExits(EmployeeExits_DTO _exit);
    }
}
