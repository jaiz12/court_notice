using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.BirthdayWishes
{
    public class BirthdayListForRealTimeDTO
    {
        public string employee_id { get; set; }
        public string full_name { get; set; }
        public string? pass_photo_url { get; set; }
        public DateTime date_of_birth { get; set; }
        public long comment_count { get; set; }

    }
}
