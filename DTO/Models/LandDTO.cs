using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Models
{
    public class LandDTO
    {
        [Key]
        public int landcase_id { get; set; }
        public DateTime application_date { get; set; }
        public string land_address { get; set; }
        public string application_number { get; set; }
        public string certificatefilepath { get; set; }
        public string land_certificate {  get; set; }
        public string parcha_no { get; set; }
        public DateTime effective_from { get; set; }
        public DateTime effective_to { get; set; }
        public List<ApplicantDTO> applicants { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
    }

    public class ApplicantDTO
    {
        [Key]
        public int id { get; set; }
        public string application_number { get; set; }
        public string applicant_name { get; set; }
        public string applicant_address { get; set; }
        public string identity_type { get; set; }
        public string identity_number { get; set; }
        public string filepath { get; set; }
        public IFormFile identity_file {  get; set; }   
        public string identity_photo { get; set; }
    }
}
