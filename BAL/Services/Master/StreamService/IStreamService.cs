using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.StreamService
{
    public interface IStreamService
    {
        Task<DataResponse> PostStream(Stream_DTO stream);
        Task<DataResponse> PutStream(Stream_DTO stream);
        Task<List<Stream_DTO>> GetStream();
        Task<DataResponse> DeleteStream(long id);
    }
}
