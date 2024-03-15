using Microsoft.AspNetCore.Identity;

namespace Rampage.Database.DomainModels;

public class AppUser:IdentityUser
{
    public string FullName { get; set; } = null!;
    public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
