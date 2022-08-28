using Microsoft.AspNetCore.Identity;

namespace Lms.Models.Authentication
{
    public class User: IdentityUser
    {
        public string? College { get; set; }
        public long? CollegeId { get; set; }
        public string FullName { get; set; }
    }
}
