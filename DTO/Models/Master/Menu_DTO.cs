using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models.Master
{
    public class Menu_DTO
    {
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Menu Name is Required")]
        public string MenuName { get; set; }
        [Required(ErrorMessage = "Parent Id is Required")]
        public long ParentId { get; set; }
        [Required(ErrorMessage = "Menu Type is Required")]
        public string MenuType { get; set; }
        public string ParentName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
