using DTO.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Models.Employee
{
    public class EmployeeProfilePhotoDTO : BaseEntityDTO
    {
        public string employee_id { get; set; }
        public string filepath { get; set; }

        [Required(ErrorMessage = "File Name is required")]
        public string filename { get; set; }
    }
}
