//== Author: Dewas Rai
//== Created_date: 03 Nov 2023
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class Mode_of_training_DTO
    {
        [Key]
        public long mode_of_training_id { get; set; }
        [Required(ErrorMessage = "Mode of Training is Required")]
        public string mode_of_training_name { get; set; }
        public string? created_by { get; set; }
        public string? updated_by { get; set; }
        public DateTime? created_on { get; set; }
        public DateTime? updated_on { get; set; }
    }
}
