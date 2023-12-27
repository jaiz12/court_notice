using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Master.Course
{
    public interface ICourseService
    {
        List<CourseDTO> Get();
        public DataResponse Post(CourseDTO item);
        public DataResponse Update(CourseDTO item);
        public Task<DataResponse> Delete(long id);
    }
}
