﻿using System;

namespace DTO.Models.BirthdayWishesDTO
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
