using DTO.Models.Common;
using DTO.Models.EmployeeOperation.PrintCV;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.EmployeeOperations.EmployeePrintCV
{
    public interface IEmployeePrintCVService
    {
        public Task<dynamic> GetEmployeeListForPrintCV(PaginationEntityDTO pagination = null);

        public Task<byte[]> GenerateEmployeeResumePdf(EmployeeDetailsForPrintCVDTO employee);

        public Task<byte[]> GenerateEmployeesResumeZip(List<EmployeeDetailsForPrintCVDTO> employeeList);
    }
}
