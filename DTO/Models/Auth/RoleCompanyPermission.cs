using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Auth
{
    public class RoleCompanyPermission_DTO
    {
        [Key]
        public long Id { get; set; }
       
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Permission Is Required")]
        public string Permission { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
