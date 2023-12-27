using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.EmployeeOperation.Transfer
{
    public class EmployeeTransferHistoryDTO
    {
        public string employee_name { get; set; }
        public long employee_transfer_log_id { get; set; }
        public string employee_id { get; set; }
        public string company { get; set; }
        public string branchoffice { get; set; }
        public string state { get; set; }
        public string district { get; set; }
        public string division { get; set; }
        public string place_of_posting { get; set; }
        public string appointment_status { get; set; }
        public string designation { get; set; }
        public DateTime effective_from { get; set; }
        public DateTime effective_to { get; set; }
        public string new_company { get; set; }
        public string new_place_of_posting { get; set; }
        public string new_branchoffice { get; set; }
        public string record_created_by { get; set; }
        public DateTime record_created_on { get; set; }
        public string record_updated_by { get; set; }
        public DateTime record_updated_on { get; set; }
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_on { get; set; }
    }
}
