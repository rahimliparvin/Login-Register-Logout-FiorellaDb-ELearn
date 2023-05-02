using Microsoft.AspNetCore.Identity;

namespace ELEARN.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool RememberMe { get; set; } = false;

    }
}
