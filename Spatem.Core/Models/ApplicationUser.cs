using Microsoft.AspNetCore.Identity;

namespace Spatem.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}