using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.ContentManagementSystem
{
    public class ContentMessageManagementSystem_DTO
    {
        [Key]
        public long message_id { get; set; }
        [Required(ErrorMessage = "Message is required")]
        public string message { get; set; }
        public DateTime created_on { get; set; }
        public string created_by { get; set; }
        public DateTime updated_on { get; set; }
        public string updated_by { get; set; }
    }
}
