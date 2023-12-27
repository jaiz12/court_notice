using DTO.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace DTO.Models.EmployeeOperation
{
    public class EmployeeExtension_Termination_DTO
    {
        public long? company_id { get; set; }
        public long? branch_id { get; set; }
        public long? state_id { get; set; }
        public long? district_id { get; set; }
        public long? place_of_posting_id { get; set; }
        public long? appointment_status_id { get; set; }
    }
    public class Extension_TerminationEmployeeService_DTO
    {
        public long employee_service_id { get; set; }
        public string employee_id { get; set; }
        public long designation_id { get; set; }
        public long company_id { get; set; }
        public long branchoffice_id { get; set; }
        public long state_id { get; set; }
        public long district_id { get; set; }
        public long division_id { get; set; }
        public long appointment_status_id { get; set; }
        public long? exit_type_id { get; set; }
        public DateTime date_of_joining { get; set; }
        public long place_of_posting { get; set; }
        public DateTime effective_from { get; set; }
        public DateTime effective_to { get; set; }
        public bool is_active { get; set; }
    }

    public class Employee_ExtensionTermination_DTO
    {
        [Key]
        public long service_extend_terminate_id { get; set; }
        [Required(ErrorMessage = "Employee id is required")]
        public long employee_service_id { get; set; }
        public string employee_id { get; set; }
        [Required(ErrorMessage = "Employee Effective To Date is required")]
        public Boolean is_active { get; set; } 
        public DateTime service_effective_to { get; set; }
        [Required(ErrorMessage = "Remark is required")]
        public string remark { get; set; }
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_on { get; set; }
    }
}
