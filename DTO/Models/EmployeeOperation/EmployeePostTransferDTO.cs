using DTO.Models.Common;
using System;

namespace DTO.Models.EmployeeOperation
{
    public class EmployeePostTransferDTO : BaseEntityDTO
    {
        public long employee_service_id { get; set; }
        public DateTime new_effective_from { get; set; }
        public long new_place_of_posting_id { get; set; }
        public long new_company_id { get; set; }
        public long? new_branchoffice_id { get; set; }
    }
}
