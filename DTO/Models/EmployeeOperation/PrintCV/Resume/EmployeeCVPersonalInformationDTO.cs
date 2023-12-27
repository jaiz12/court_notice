using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.EmployeeOperation.PrintCV.Resume
{
    public class EmployeeCVPersonalInformationDTO
    {
        public string employee_name {  get; set; }
        public string date_of_birth { get; set; }
        public string gender_name { get; set; }
        public long mobile_number { get; set; }
        public string correspondence_address { get; set; }
        public string permanent_address { get; set; }
        public string personal_email_id { get; set; }
    }
}
