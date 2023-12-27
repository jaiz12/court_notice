// =============================================
// -- Author:		Mukesh Shah
// -- Create date: 01-Nov-2023
// =============================================
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class BloodGroup_DTO
    {
        [Key]
        public long blood_group_id { get; set; }

        [Required(ErrorMessage = "Blood Group Name is required")]
        public string blood_group_name { get; set; }
        public DateTime? created_on { get; set; }
        public string? created_by { get; set; }
        public DateTime? updated_on { get; set; }
        public string? updated_by { get; set; }
    }
}
