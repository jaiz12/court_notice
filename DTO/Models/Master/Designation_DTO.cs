using DTO.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class Designation_DTO : BaseEntityDTO
    {
        [Key]
        public long designation_id { get; set; }

        [Required(ErrorMessage = "Designation name is required")]
        public string designation_name { get; set; }

    }
}
