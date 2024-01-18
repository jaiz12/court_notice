using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Models.Chathub
{
    public class ChatHubDTO
    {
        public long? Id { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
