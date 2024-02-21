﻿using System;

namespace DTO.Models.BirthdayWishesDTO
{
    public class BirthdayWishesDTO
    {
        public string employee_id { get; set; }
        public DateTime? created_on { get; set; }
        public string comment { get; set; }
        public string comment_employee_id { get; set; }
        public long? birthday_comment_id { get; set; }
    }
}
