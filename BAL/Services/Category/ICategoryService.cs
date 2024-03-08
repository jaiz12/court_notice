using DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Category
{
    public interface ICategoryService
    {
        Task<DataResponse> Post(CategoryDTO categoryDTO);
        Task<DataResponse> Put(CategoryDTO categoryDTO);
        Task<List<CategoryDTO>> Get();
        Task<DataResponse> Delete(long department_id);
    }
}
