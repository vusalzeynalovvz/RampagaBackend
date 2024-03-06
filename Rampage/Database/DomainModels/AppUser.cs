using Microsoft.AspNetCore.Identity;

namespace Rampage.Database.DomainModels;

public class AppUser:IdentityUser
{
    public string FullName { get; set; } = null!;
}
