using DTO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Marriage
{
    public interface IMarriageService
    {
        Task<DataResponse> Post(MarriageDTO marriage);
        Task<DataTable> Get();
        Task<DataResponse> Delete(long marriage_id);
    }
}
