using DTO.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class ExitType_DTO : BaseEntityDTO
    {
        [Key]
        public long exit_type_id { get; set; }

        [Required(ErrorMessage = "Exit Type name is required")]
        public string exit_type_name { get; set; }

    }
}
