using DTO.Models.Common;

namespace DTO.Models.Employee
{
    public class EmployeeProfessionalQualificationDetailsDTO : BaseEntityDTO
    {
        public long employee_professional_qualification_id { get; set; }
        public string employee_id { get; set; }
        public string course_name { get; set; }
        public long? mode_of_training_id { get; set; }
        public int? year { get; set; }
        public int? course_duration { get; set; }
        public bool? in_service { get; set; }
        public string attachment_url { get; set; }
    }
}
