using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.PlaceOfPostingService
{
    public interface IPlaceOfPostingService
    {
        Task<DataResponse> PostPlaceOfPosting(PlaceOfPosting_DTO _placeOfPosting);
        Task<DataResponse> PutPlaceOfPosting(PlaceOfPosting_DTO _placeOfPosting);
        Task<List<PlaceOfPosting_DTO>> GetPlaceOfPosting();
        Task<DataResponse> DeletePlaceOfPosting(long id);
    }
}
