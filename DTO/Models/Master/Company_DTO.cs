using DTO.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class Company_DTO : BaseEntityDTO
    {
        [Key]
        public long company_id { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        public string company_name { get; set; }

    }
}
