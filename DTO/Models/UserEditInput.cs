/*
 * Author: Gautam Sharma
 * Date: 15-05-2021
 * Desc: User Input class to hold ui values
 */
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models
{
    public class UserEditInput
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// User Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// User's First Name
        /// </summary>
        [Required(ErrorMessage = "UserName is required")]
        public string FirstName { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// User's Last Name
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Phone Number for User
        /// </summary> 
        public string PhoneNumber { get; set; }

        /// <summary> 
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Active Status
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// User's Role
        /// </summary>
        //public virtual ICollection<RolesInputModel> Roles { get; set; }

        public List<SelectedModules> Modules { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class SelectedModules
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Module Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Name of Module
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Status holding Selected?
        /// </summary>
        public bool Checked { get; set; }

        public virtual ICollection<SelectedOperations> ModuleOperations { get; set; }
    }

    public class SelectedOperations
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Operation ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Operation Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Operation Checked 
        /// </summary>
        public bool Checked { get; set; }

        /// <summary> 
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// List of permissions user holds
        /// </summary>
        public virtual ICollection<SelectedPermissions> Permissions { get; set; }
    }

    public class SelectedPermissions
    {
        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Status Check
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Permimssion Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Author: Gautam Sharma
        /// Date: 25-05-2021
        /// Actual Permission
        /// </summary>
        public string Permissions { get; set; }
    }
}
