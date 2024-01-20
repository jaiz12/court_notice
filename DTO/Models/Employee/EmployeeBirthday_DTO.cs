using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public DateTime? created_on { get; set; }

        [Required(ErrorMessage = "Message content is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Message must be between 1 and 100 characters.")]
        public string comment { get; set; }
        public string comment_employee_id { get; set; }
        public string emp_first_name { get; set; }
        public string emp_middle_name { get; set; }
        public string emp_last_name { get;set; }
        public string commenter_first_name { get; set; }
        public string commenter_middle_name { get; set; }
        public string commenter_last_name { get; set; }
        public string commenter_pass_photo_url { get; set; }
        public long birthday_comment_id { get; set; }
    }
}
