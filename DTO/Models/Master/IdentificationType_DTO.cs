using DTO.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class IdentificationType_DTO : BaseEntityDTO
    {
        [Key]
        public long identificationtype_id { get; set; }

        [Required(ErrorMessage = "Identification Type name is required")]
        public string identification_type_name { get; set; }

    }
}
