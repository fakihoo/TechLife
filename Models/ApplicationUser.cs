using Microsoft.AspNetCore.Identity;

namespace TechLife.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ProfilePictureUrl { get; set; }
    }
}
