using DTO.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class AppointmentStatusDTO : BaseEntityDTO
    {
        [Key]
        public long appointment_status_id { get; set; }

        [Required(ErrorMessage = "Appointment Status Name is Required")]
        public string appointment_status_name { get; set; }
    }
}
