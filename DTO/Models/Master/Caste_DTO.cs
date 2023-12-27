using DTO.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class Caste_DTO : BaseEntityDTO
    {
        [Key]
        public long caste_id { get; set; }

        [Required(ErrorMessage = "Caste name is required")]
        public string caste_name { get; set; }

    }

}
