using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class Boards_DTO
    {
        [Key]
        public long boards_id { get; set; }
        [Required(ErrorMessage = "Boards is Required")]
        public string boards_name { get; set; }
        public string? created_by { get; set; }
        public string? updated_by { get; set; }
        public DateTime? created_on { get; set; }
        public DateTime? updated_on { get; set; }
    }
}
