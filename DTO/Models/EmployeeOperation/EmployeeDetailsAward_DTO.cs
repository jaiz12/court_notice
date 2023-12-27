using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.EmployeeOperation
{
    public class EmployeeDetailsAward_DTO
    {
        public string employee_id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }

        public long company_id { get; set; }
        public string company_name { get; set;}

        public long division_id { get; set; }
        public string division_name { get; set;}

        public long place_of_posting_id { get; set; }
        public string place_of_posting_name { get; set; }

        public long designation_id { get; set; }
        public string designation_name { get; set; }

    }
}
