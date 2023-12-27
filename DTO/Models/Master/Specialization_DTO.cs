using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class Specialization_DTO
    {
        [Key]
        public long specialization_id { get; set; }
        [Required(ErrorMessage = "Specialization is Required")]
        public string specialization_name { get; set; }
        public string? created_by { get; set; }
        public string? updated_by { get; set; }
        public DateTime? created_on { get; set; }
        public DateTime? updated_on { get; set; }
    }
}
