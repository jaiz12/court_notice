using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Models.Auth
{
    public class AspNetUsers_Register_DTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string Id { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Effictive Date is Required")]
        public DateTime effective_from { get; set; }
        [Required(ErrorMessage = "State is Required")]
        public long state { get; set; }
        public string? stateName { get; set; }
        [Required(ErrorMessage = "District is Required")]
        public long district { get; set; }
        public string? districtName { get; set; }
        [Required(ErrorMessage = "Company is Required")]
        public long company { get; set; }
        public string? companyName { get; set; }
        [Required(ErrorMessage = "Branch is Required")]
        public long branch { get; set; }
        public string? branchName { get; set; }
        [Required(ErrorMessage = "Employee Code is Required")]
        public string employeeCode { get; set; }
        [Required(ErrorMessage = "Date Of Birth is Required")]
        public DateTime dob { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        public long gender { get; set; }
        public string? genderName { get; set; }
        [Required(ErrorMessage = "Appointment Status is Required")]
        public long appointmentStatus { get; set; }
        public string? appointmentStatusName { get; set; }
        [Required(ErrorMessage = "Designation is Required")]
        public long designation { get; set; }
        public string? designationName { get; set; }
        [Required(ErrorMessage = "division is Required")]
        public long division { get; set; }
        public string? divisionName { get; set; }
        [Required(ErrorMessage = "Place Of Posting is Required")]
        public long placeOfPosting { get; set; }
        public string? placeOfPostingName { get; set; }
        [Required(ErrorMessage = "Date Of Joining is Required")]
        public DateTime dateOfJoining { get; set; }
        [Required(ErrorMessage = "Role is Required")]
        public List<AspNetUserRoles_Register_DTO> Roles { get; set; }
        public bool UserStatus { get; set; }

        public string? exit_type_id {  get; set; }
        public long? employee_personal_id { get; set; }
        public long? employee_service_id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool is_active { get; set; }


    }

    public class AspNetUsers_Login_DTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }
        public List<AspNetUserRoles_Register_DTO> Roles { get; set; }

        public List<AspNetUserRolePermission_DTO> aspNetUserRolePermission { get; set; }

    }


    public class AspNetUserRoles_Register_DTO
    {
        [Key]
        public long Id { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "Role Name is Required")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Company Name is Required")]
        public string CompanyName { get; set; }

    }

    public class AspNetUserRolePermission_DTO
    {
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string Permissions { get; set; }
    }

    public class ChangePassword
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Old Password cannot be empty")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Password cannot be empty")]
        public string password { get; set; }
        [Required(ErrorMessage = "Password cannot be empty")]
        public string confirmpassword { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Id { get; set; }
    }


    public class FinancialYear_DTO
    {
        [Key]
        public long financial_year_id { get; set; }
        [Required(ErrorMessage = "Financial Year Start Date is required")]
        public DateTime from_date { get; set; }
        [Required(ErrorMessage = "Financial Year End Date is required")]
        public DateTime to_date { get; set; }
        public string financial_year { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
    }

    public class LeaveConfiguration_DTO
    {
        [Key]
        public long leave_configuration_id { get; set; }
        public long company_id { get; set; }
        public string company_name { get; set; }
        public long branch_id { get; set; }
        public string branch_name { get; set; }
        public long appointment_status_id { get; set; }
        public string appointment_status_name { get; set; }
        public long? gender_id { get; set; }
        public string gender_name { get; set; }
        public long financial_year_id { get; set; }
        public string financial_year { get; set; }
        public long leave_type_id { get; set; }
        public string leave_type_name { get; set; }
        public bool yearly_carry_forward { get; set; }
        public bool monthly_carry_forward { get; set; }
        public long max_leave_per_month { get; set; }
        public long increment_count { get; set; }
        public long increment_after_days { get; set; }
        public long max_leave_per_year { get; set; }
        public bool partial_carry { get; set; }
        public long count { get; set; }
        public bool full_carry { get; set; }
        public string? created_by { get; set; }
        public DateTime? created_on { get; set; }
        public string? updated_by { get; set; }
        public DateTime? updated_on { get; set; }
    }

}
