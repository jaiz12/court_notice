using DTO.Models.Common;

namespace DTO.Models.Employee
{
    public class EmployeeOtherDetailsDTO : BaseEntityDTO
    {
        public long? employee_other_id { get; set; }
        public string employee_id { get; set; }
        public string? reporting_manager_id { get; set; }
        public long? bank_account_no { get; set; }
        public long? pf_uan_no { get; set; }
        public long? pf_bank_account_no { get; set; }
        public string pan_no { get; set; }
    }
}
