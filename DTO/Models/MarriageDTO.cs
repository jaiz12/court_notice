using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Models
{
    public class MarriageDTO
    {
        [Key]
        public long? marriage_id { get; set; }
        [Required(ErrorMessage = "Application Date is Required")]
        public DateTime application_date {  get; set; }
        [Required(ErrorMessage = "Application Number is Required")]
        public string application_no {  get; set; }
        [Required(ErrorMessage = "Husband Name is Required")]
        public string husband_name {  get; set; }
        [Required(ErrorMessage = "Wife Name is Required")]
        public string wife_name {  get; set; }
        [Required(ErrorMessage = "Husband Address is Required")]
        public string husband_address { get; set; }

        [Required(ErrorMessage = "Wife Address is Required")]
        public string wife_address { get; set; }
        public string husbandfilepath { get; set; }
        [Required(ErrorMessage = "Husband Photo is Required")]
        public string husband_photo { get; set; }
        public string wifefilepath { get; set; }
        [Required(ErrorMessage = "Wife Photo is Required")]
        public string wife_photo { get; set; }
        [Required(ErrorMessage = "Effective From Date is Required")]
        public string effective_from {  get; set; }
        [Required(ErrorMessage = "Effective To Date is Required")]
        public string effective_to { get; set; }
        [Required(ErrorMessage = "Marriage Certificate Photo is Required")]
        public string marriage_certificate_photo {  get; set; }
        public string marriage_certificate { get; set; }
        public string marriage_certificate_path { get; set; }
        public string? created_by { get; set; }
        public DateTime? created_on { get; set; }
        public string? updated_by { get; set; }
        public DateTime? updated_on { get; set; }

    }
}
