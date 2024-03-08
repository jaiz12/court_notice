using DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Designation
{
    public interface IDesignationService
    {
        Task<DataResponse> Post(DesignationDTO designationDTO);
        Task<DataResponse> Put(DesignationDTO designationDTO);
        Task<List<DesignationDTO>> Get();
        Task<DataResponse> Delete(long designation_id);
    }
}
