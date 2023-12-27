using DTO.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class District_DTO : BaseEntityDTO
    {
        [Key]
        public long district_id { get; set; }

        [Required(ErrorMessage = "District name is required")]
        public string district_name { get; set; }

        public long state_id { get; set; }

    }
}
