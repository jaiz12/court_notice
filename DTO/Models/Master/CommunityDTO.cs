using DTO.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class CommunityDTO : BaseEntityDTO
    {
        [Key]
        public long community_id { get; set; }

        [Required(ErrorMessage = "Community Name is Required")]
        public string community_name { get; set; }
    }
}
