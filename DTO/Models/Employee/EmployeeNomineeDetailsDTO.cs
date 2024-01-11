using DTO.Models.Common;
using System;

namespace DTO.Models.Employee
{
    public class EmployeeNomineeDetailsDTO : BaseEntityDTO
    {
        public long? employee_nominee_id { get; set; }
        public string employee_id { get; set; }
        public string nominee_name { get; set; }
        public DateTime date_of_birth { get; set; }
        public string relation { get; set; }
        public decimal? percentage { get; set; }
        public long phone { get; set; }
        public string address { get; set; }
    }
}
