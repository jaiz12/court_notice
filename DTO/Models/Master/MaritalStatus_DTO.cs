//== Author: Dewas Rai
//== Created_Date: 02 November 2023
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class MaritalStatus_DTO
    {
        [Key]
        public long marital_status_id { get; set; }
        [Required(ErrorMessage = "Marital Status is Required")]
        public string marital_status_name { get; set; }
        public string? created_by { get; set; }
        public string? updated_by { get; set; }
        public DateTime? created_on { get; set; }
        public DateTime? updated_on { get; set; }
    }
}
