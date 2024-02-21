using DTO.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Employee
{
    public class EmployeePersonalDetailsDTO : BaseEntityDTO
    {
        public long? employee_personal_id { get; set; }

        [Key]
        [Required(ErrorMessage = "Employee ID is Required")]
        public string employee_id { get; set; }
        public string first_name { get; set; }
        public string? middle_name { get; set; }
        public string last_name { get; set; }
        public string? pass_photo_url { get; set; }
        public string? father_name { get; set; }
        public string? mother_name { get; set; }
        public DateTime date_of_birth { get; set; }
        public long gender_id { get; set; }
        public long? marital_status_id { get; set; }
        public long? blood_group_id { get; set; }
        public long? community_id { get; set; }
        public long? religion_id { get; set; }
        public string? nationality_name { get; set; }
        public string address { get; set; }
    }
}
