using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.EmployeeOperation
{
    public class Employee_AppointmentStatus_DTO
    {
        public long? company_id { get; set; }
        public long? branch_id { get; set; }
        public long? state_id { get; set; }
        public long? district_id { get; set; }
        public long? place_of_posting_id { get; set; }
    }

    public class Employee_AppointmentStatus_DTO_Edit
    {
        public string employee_id { get; set; }
        public long? appointment_status_id { get; set; }
        public string updated_by { get; set; }
    }
}
