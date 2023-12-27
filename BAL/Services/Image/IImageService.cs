using DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Image
{
    public interface IImageService
    {
        Task<DataResponse> PostAsync(Image_DTO data);
        Task<DataResponse> PutAsync(int? id, Image_DTO data);
        Task<List<Image_DTO>> GetAsync();
    }
}
