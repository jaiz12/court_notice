using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.EmployeeOperation.PrintCV.Resume
{
    public class EmployeeCVQualificationDTO
    {
        public string qualification_name {  get; set; }

        public string stream_name { get; set; }

        public string school_name { get; set; }

        public int year_of_passing { get; set; }

        public Decimal percentage { get; set; }
    }
}
