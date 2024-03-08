using DTO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Land
{
    public interface ILandService
    {
        Task<DataResponse> Post(LandDTO landDTO);
        Task<object> Get();
        Task<DataResponse> Delete(long application_number);
    }
}
