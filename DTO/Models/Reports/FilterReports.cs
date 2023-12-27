using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.Reports
{
    public class FilterReports
    {
        public long company_id { get; set; }
        public long branch_id { get; set;}
        public long division_id { get; set;}
        public long state_id { get; set;}
        public long district_id { get; set;}
        public long place_of_posting_id { get; set;}
        public long appointment_status_id { get; set;}
        public string active { get; set;}
    }
    public class EmpLeaveHistoryFilter
    {
        public long company_id { get; set; }
        public long branch_id { get; set;}
        public long place_of_posting_id { get; set;}
        public long division_id { get; set;}
        public long designation_id { get; set;}
        public long appointment_status_id { get; set;}
        public long leave_type_id { get; set;}
        public long month { get; set; }
    }
}
