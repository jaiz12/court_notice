// =============================================
// -- Author:		Mukesh Shah
// -- Create date: 01-Nov-2023
// =============================================
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class Division_DTO
    {
        [Key]
        public long division_id { get; set; }
        [Required(ErrorMessage = "Division Name is required")]
        public string division_name { get; set; }
        public long company_id { get; set; }
        public string company_name { get; set; }
        public DateTime created_on { get; set; }
        public string created_by { get; set; }
        public DateTime updated_on { get; set; }
        public string updated_by { get; set; }
    }
}
