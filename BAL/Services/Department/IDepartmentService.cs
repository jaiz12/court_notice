using DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Department
{
    public interface IDepartmentService
    {
        Task<DataResponse> Post(DepartmentDTO departmentDTO);
        Task<DataResponse> Put(DepartmentDTO departmentDTO);
        Task<List<DepartmentDTO>> Get();
        Task<DataResponse> Delete(long department_id);
    }
}
