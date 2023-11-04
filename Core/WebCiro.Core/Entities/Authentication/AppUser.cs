using Microsoft.AspNetCore.Identity;

namespace WebCiro.Core.Entities.Authentication
{

    public class AppUser : IdentityUser
    {
        public string? IPAddress { get; set; }
        public string? MaccAdress { get; set; }
        public string? UserIdentity { get; set; }

    }
}
