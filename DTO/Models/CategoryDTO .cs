using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO.Models
{
    public class CategoryDTO
    {
        [Key]
        public long category_id { get; set; }
        [Required(ErrorMessage = "Category Name is Required")]
        public string category_name { get; set; }
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string updated_by { get; set;}
        public DateTime updated_on { get; set;}

    }
}
