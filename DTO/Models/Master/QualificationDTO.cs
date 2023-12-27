using DTO.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class QualificationDTO : BaseEntityDTO
    {
        [Key]
        public long qualification_id { get; set; }

        [Required(ErrorMessage = "Qualification Name is Required")]
        public string qualification_name { get; set; }
    }
}
