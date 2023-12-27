using DTO.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class ResidentialStatusDTO : BaseEntityDTO
    {
        [Key]
        public long residential_status_id { get; set; }

        [Required(ErrorMessage = "Residential Status Name is Required")]
        public string residential_status_name { get; set; }
    }
}
