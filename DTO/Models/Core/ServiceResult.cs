/*
 * Author : Gautam Sharma
 * Date: 25-05-2021
 * Desc: Service Model file for API/DI
 */

using System.Collections.Generic;

namespace DTO.Models.Core
{
    public class ServiceResult
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Pass errors in array object, for UI and API handling 
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Automatically parsed. No need to call and update in any service.
        /// </summary>
        public bool IsSuccessful => Errors == null || Errors.Count <= 0;

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Use this Property when passing extras values. (Not necessarily needed)
        /// </summary>
        public object Extras { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Reference Id for Object, API, Service Reference
        /// </summary>
        public string RefId { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Pass Service Data as object to API then to UI
        /// Any Class/Model can be parsed here when using object
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Response Message
        /// </summary>
        public string Message { get; set; }

        public ServiceResult(string message = "")
        {
            Message = message;
        }

    }
}
