using DTO.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class CompanyLogoDTO : BaseEntityDTO
    {
        [Key]
        public long company_id { get; set; }
        public string company_name { get; set; }
        public string company_logo_url { get; set; }
    }
}
