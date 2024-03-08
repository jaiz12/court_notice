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
        

        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }
        public string user_name { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Designation is required")]
        public long designation_id { get; set; }
        public string designation_name {  get; set; }
        [Required(ErrorMessage = "Department is required")]
        public long department_id { get; set; }
        public string department_name {  get; set; }
        public bool UserStatus { get; set; }
        public List<AspNetUserRoles_Register_DTO> Roles { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

    public class AspNetUsers_Edit_DTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string Id { get; set; }


        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Designation is required")]
        public long designation_id { get; set; }
        public string designation_name { get; set; }
        [Required(ErrorMessage = "Department is required")]
        public long department_id { get; set; }
        public string department_name { get; set; }
        public bool UserStatus { get; set; }
        public List<AspNetUserRoles_Register_DTO> Roles { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool is_active { get; set; }
    }

    public class AspNetUserRoles_Register_DTO
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Role Name is Required")] 
        public string rolename { get; set; }

    }

    public class AspNetUsers_Login_DTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        public List<AspNetUserRoles_Register_DTO> Roles { get; set; }

    }
}
