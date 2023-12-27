using System;

namespace DTO.Models.Common
{
    public class BaseEntityDTO
    {
        public string? created_by { get; set; }
        public DateTime? created_on { get; set; }
        public string? updated_by { get; set; }
        public DateTime? updated_on { get; set; }
    }
}
