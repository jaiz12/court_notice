using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.EmployeeOperation.PrintCV
{
    public class EmployeeDetailsForPrintCVDTO
    {
        public string employee_id { get; set; }
        public long employee_service_id { get; set; }
        public string employee_name { get; set; }
        public string pass_photo_url { get; set; }
        public string company_name { get; set; }
        public string branch_name { get; set; }
        public string designation_name { get; set; }
        public string division_name { get; set; }
        public string place_of_posting_name { get; set; }
        public string appointment_status_name { get; set; }
    }
}
