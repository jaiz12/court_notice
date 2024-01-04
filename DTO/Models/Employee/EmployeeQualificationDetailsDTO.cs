﻿using DTO.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Employee
{
    public class EmployeeQualificationDetailsDTO : BaseEntityDTO
    {
        [Key]
        public long? employee_qualification_id { get; set; }
        public string employee_id { get; set; }
        public long? qualification_id { get; set; }
        public string institute_name { get; set; }
        public int year_of_passing { get; set; }
        public long? board_id { get; set; }
        public string specialization_name { get; set; }
        public string stream_name { get; set; }
        public string grade { get; set; }
        public decimal? percentage { get; set; }
        public string? attachment_url { get; set; }
    }
}
