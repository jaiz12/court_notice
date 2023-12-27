using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.EmployeeOperation
{
    public class EmployeePromotionOptionValues_DTO
    {
        public long? company_id { get; set; }
        public long? branch_id { get; set; } 
        public long? state_id { get; set; } 
        public long? district_id { get; set; }
        public long? place_of_posting_id { get; set; }
        public long? appointment_status_id { get; set; }
    }

    public class PromotedEmployeeService_DTO
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
        public long place_of_posting_id { get; set; }
        public DateTime effective_from { get; set; }
        public DateTime effective_to { get; set; }
        public bool is_active { get; set; }

        public string? created_by { get; set; }
        public DateTime? created_on { get; set; }
        public string? updated_by { get; set; }
        public DateTime? updated_on { get; set; }
    }

    public class EditPromotedEmployee_DTO
    {
        public long employee_service_id { get; set; }
        public string employee_id { get; set; }
        public long designation_id { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_on { get; set; }
    }
}
