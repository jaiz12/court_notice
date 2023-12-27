using DTO.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class InServiceTraining_DTO : BaseEntityDTO
    {
        [Key]
        public long inservicetraining_id { get; set; }

        [Required(ErrorMessage = "InService Training name is required")]
        public string inservice_training_name { get; set; }

    }
}
