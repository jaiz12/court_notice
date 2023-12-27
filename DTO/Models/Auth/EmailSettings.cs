using System.Collections.Generic;

namespace DTO.Models.Auth
{
    public class EmailSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        
        public List<SenderDetails> senderDetails { get; set; }
    }
    public class SenderDetails
    {
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
    }
}
