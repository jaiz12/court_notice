using DTO.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.EmployeeOperation.Transfer
{
    public class EmployeeTransferDTO : EmployeePostTransferDTO
    {
        public string employee_id { get; set; }
        public long employee_personal_id { get; set; }
        public string employee_name { get; set; }
        public DateTime date_of_birth { get; set; }
        public long gender_id { get; set; }
        public long company_id { get; set; }
        public long division_id { get; set; }
        public long place_of_posting_id { get; set; }
        public long designation_id { get; set; }
        public long branchoffice_id { get; set; }
        public DateTime effective_from { get; set; }
        public DateTime effective_to { get; set; }
    }
}
