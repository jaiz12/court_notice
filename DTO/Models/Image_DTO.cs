using System.ComponentModel.DataAnnotations;

namespace DTO.Models
{
    public class Image_DTO
    {
        [Key]
        public long Id { get; set; }
        public string filepath { get; set; }
        [Required(ErrorMessage = "File Name is required")]
        public string filename { get; set; }
        public string UserName { get; set; }
    }
}
