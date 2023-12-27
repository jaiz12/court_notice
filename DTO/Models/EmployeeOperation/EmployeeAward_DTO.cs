using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.EmployeeOperation
{
    public class EmployeeAward_DTO
    {
        [Key]
        public long employee_award_id { get; set; }
        [Required(ErrorMessage = "Employee Award Name is required")]
        public string employee_award_name { get; set; }
        [Required(ErrorMessage = "Employee Award Date is required")]
        public DateTime employee_award_date { get; set; }
        [Required(ErrorMessage = "Employee Effective To Date is required")]
        public DateTime employee_award_month { get; set; }
        public string? award_description { get; set; }
        public string employee_id { get; set; }
        public string employee_name { get; set; }
        public string company_name { get; set; }
        public string state_name { get; set; }
        public string division_name { get; set; }
        public string place_of_posting_name { get; set; }
        public string designation_name { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
    }
    public class EmployeeAwardEdit_DTO
    {
        public long employee_award_id { get; set; }
        public string employee_id { get; set; }
        public string employee_award_name { get; set; }
        public DateTime employee_award_date { get; set; }
        public DateTime employee_award_month { get; set; }
        public string? award_description { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_on { get; set; }
    }
    public class EmployeeAwardFilter_DTO
    {
        public long company_id { get; set; }
        public long state_id { get; set; }
        public long district_id { get; set; }
        public long branch_id { get; set; }
        public long place_of_posting_id { get; set; }
        public long appointment_status_id { get; set; }
    }
    public class EmpAwardFilter_DTO
    {
        public string company_name { get; set; }
        public string state_name { get; set; }
        public string place_of_posting_name { get; set; }
        public string designation_name { get; set; }
        public string division_name { get; set; }
    }
}
