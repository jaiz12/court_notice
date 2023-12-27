using DTO.Models.Common;

namespace DTO.Models.Employee
{
    public class EmployeeIdentificationDetailsDTO : BaseEntityDTO
    {
        public long? employee_identification_id { get; set; }
        public string employee_id { get; set; }
        public long? identification_type_id { get; set; }
        public string identification_number { get; set; }
        public string? attachment_url { get; set; }
    }
}
