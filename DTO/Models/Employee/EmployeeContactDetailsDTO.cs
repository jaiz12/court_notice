using DTO.Models.Common;

namespace DTO.Models.Employee
{
    public class EmployeeContactDetailsDTO : BaseEntityDTO
    {
        public string employee_id { get; set; }
        public long? mobile_number { get; set; }
        public long? alternative_mobile_number { get; set; }
        public string work_email_id { get; set; }
        public string personal_email_id { get; set; }
        public string permanent_address { get; set; }
        public string correspondance_address { get; set; }
        public string emergency_contact_a_name { get; set; }
        public string emergency_contact_a_relation { get; set; }
        public long? emergency_contact_a_phone { get; set; }
        public string emergency_contact_b_name { get; set; }
        public string emergency_contact_b_relation { get; set; }
        public long? emergency_contact_b_phone { get; set; }
    }
}
