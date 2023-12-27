using DTO.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Employee
{
    public class EmployeeServiceDetailsDTO : BaseEntityDTO
    {
        [Key]
        public long? employee_service_id { get; set; }
        [Required(ErrorMessage = "Employee ID is Required")]
        public string employee_id { get; set; }
        public long company_id { get; set; }
        public long branchoffice_id { get; set; }
        public long state_id { get; set; }
        public long district_id { get; set; }
        public long designation_id { get; set; }
        public long division_id { get; set; }
        public long appointment_status_id { get; set; }
        public long? exit_type_id { get; set; }
        public DateTime date_of_joining { get; set; }
        public long place_of_posting_id { get; set; }
        public DateTime effective_from { get; set; }
        public DateTime effective_to { get; set; }
        public bool is_active {  get; set; }
    }
}
