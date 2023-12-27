using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.Employee
{
    public class EmployeeBirthday_DTO
    {
        public string employee_id { get; set; }
        public string full_name { get; set; }
        public string? pass_photo_url { get; set; }
        public DateTime date_of_birth { get; set; }
        public long days_until_birthday { get; set; }
        public string birthday_status { get; set; }
        public long company_id { get; set; }
        public string company_name { get; set; }
        public long branch_id { get; set; }
        public string branch_name { get; set; }
        public long state_id { get; set; }
        public string state_name { get; set; }
    }
}
