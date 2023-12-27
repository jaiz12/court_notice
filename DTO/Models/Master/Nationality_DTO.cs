// =============================================
// -- Author:		Mukesh Shah
// -- Create date: 02-Nov-2023
// =============================================
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class Nationality_DTO
    {
        [Key]
        public long nationality_id { get; set; }
        [Required(ErrorMessage = "Nationality Name is required")]
        public string nationality_name { get; set; }
        public DateTime created_on { get; set; }
        public string created_by { get; set; }
        public DateTime updated_on { get; set; }
        public string updated_by { get; set; }
    }
}
