/*
 * Author: Gautam Sharma
 * Date: 15-05-2021
 * Desc: SMS settings values 
 */

namespace DTO.Models
{
    // Added by Gautam
    public class SMSSettings
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// User Id/Name or can be anything that is unique for User
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// SMS Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Sender Id provided by Gateway
        /// </summary>
        public string SenderId { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Url for SMS sender
        /// </summary>
        public string Url { get; set; }
    }
}
