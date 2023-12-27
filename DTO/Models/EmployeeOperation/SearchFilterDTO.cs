using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.EmployeeOperation
{
    public class SearchFilterDTO
    {
        public string state { get; set; } = string.Empty;
        public string district { get; set; } = string.Empty;
        public string company { get; set; } = string.Empty;
        public string branch { get; set; } = string.Empty;
        public string place_of_posting { get; set; } = string.Empty;
        public string appointment_status { get; set; } = string.Empty;
        public string searchTerm { get; set; } = string.Empty;
    }
}
