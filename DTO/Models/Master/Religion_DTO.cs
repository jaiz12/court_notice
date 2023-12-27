//== Author: Dewas Rai
//== Created_Date: 02 November 2023
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class Religion_DTO
    {
        [Key]
        public long religion_id { get; set; }
        [Required(ErrorMessage = "Religion is Required")]
        public string religion_name { get; set; }
        public string? created_by { get; set; }
        public string? updated_by { get; set; }
        public DateTime? created_on { get; set; }
        public DateTime? updated_on { get; set; }
    }
}
