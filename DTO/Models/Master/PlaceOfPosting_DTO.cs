using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class PlaceOfPosting_DTO
    {
        [Key]
        public long place_of_posting_id { get; set; }
        public long state_id { get; set; }
        public string state_name { get; set; }
        public long district_id { get; set; }
        public string district_name { get; set; }
        public string place_of_posting_name { get; set; }
        [Required(ErrorMessage = "Place Of Posting name is required")]
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
    }
}
