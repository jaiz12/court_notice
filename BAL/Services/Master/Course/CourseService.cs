using BAL.Services.Master.Common;
using Common.Utilities;
using DTO.Models;
using DTO.Models.Master;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BAL.Services.Master.Course
{
    internal class CourseService : ICourseService
    {

        private IMasterCommonService _commonService;
        public CourseService(IMasterCommonService commonService)
        {
            _commonService = commonService;
        }

        public List<CourseDTO> Get()
        {
            DataTable dt = _commonService.Get("emp_get_course_master");
            return DataTableVsListOfType.ConvertDataTableToList<CourseDTO>(dt);
        }

        public DataResponse Post(CourseDTO item)
        {
            return _commonService.PostOrUpdate("emp_post_course_master", "course", item, false);
        }

        public DataResponse Update(CourseDTO item)
        {
            return _commonService.PostOrUpdate("emp_update_course_master", "course ", item, true);
        }

        public async Task<DataResponse> Delete(long id)
        {
            return await _commonService.Delete("emp_delete_course_master", "course", id);
        }
    }
}
