// =============================================
// -- Author:		Mukesh Shah
// -- Create date: 03-Nov-2023
// =============================================

using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class TrainingType_DTO
    {
        [Key]
        public long training_type_id { get; set; }
        [Required(ErrorMessage = "training type Name is required")]
        public string training_type_name { get; set; }
        public DateTime created_on { get; set; }
        public string created_by { get; set; }
        public DateTime updated_on { get; set; }
        public string updated_by { get; set; }
    }
}
