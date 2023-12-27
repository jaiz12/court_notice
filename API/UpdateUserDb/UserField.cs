using Microsoft.AspNetCore.Identity;

namespace API.UpdateUserDb
{
    public class UserField : IdentityUser
    {
        public string CandidateName { get; set; }
        public bool userstatus { get; set; }
        public string createdby { get; set; }
        public string updatedby { get; set; }
        public string createdon { get; set; }
        public string updatedon { get; set; }
    }
}
