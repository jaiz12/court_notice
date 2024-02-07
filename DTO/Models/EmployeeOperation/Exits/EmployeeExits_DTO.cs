using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.EmployeeOperation.Exits
{
    public class EmployeeExits_DTO
    {
        public long employee_service_id { get; set; }
        public string employee_id { get; set; }
        public DateTime effective_to { get; set; }
        public Boolean UserStatus { get; set; }
        public int button_id { get; set; }
        public string UserName { get; set; }
        
    }
    public class EmployeeExitsOptionValue_DTO
    {
        public long company_id { get; set; }
        public long branch_id { get; set; }
        public long state_id { get; set; }
        public long district_id { get; set; }
        public long place_of_posting_id { get; set; }
        public long appointment_status_id { get; set; }
    }
}
