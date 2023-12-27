using DTO.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class State_DTO : BaseEntityDTO
    {
        [Key]
        public long state_id { get; set; }

        [Required(ErrorMessage = "State name is required")]
        public string state_name { get; set; }

    }
}
