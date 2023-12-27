using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class Branch_DTO
    {
        [Key]
        public long branch_id { get; set; }
        [Required(ErrorMessage = "Branch/Office Name is required")]
        public string branch_name { get; set; }
        public long company_id { get; set; }
        public string company_name { get; set; }
        public DateTime created_on { get; set; }
        public string created_by { get; set; }
        public DateTime updated_on { get; set; }
        public string updated_by { get; set; }
    }
}
