using DTO.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class CourseDTO : BaseEntityDTO
    {
        [Key]
        public long course_id { get; set; }

        [Required(ErrorMessage = "Course Name is Required")]
        public string course_name { get; set; }
    }
}
