using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Models.ContentManagementSystem
{
    public class ContentManagementSystem_DTO
    {
        [Key]
        public long cms_id { get; set; }
        [Required(ErrorMessage = "CMS Title is required")]
        public string cms_title { get; set; }
        [Required(ErrorMessage = "CMS Desciption is required")]
        public string cms_description { get; set;}
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set;}
        public string updated_by { get; set; }
        public string created_by { get; set;}
    }
}
