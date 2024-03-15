using NuGet.Protocol.Core.Types;

namespace Rampage.Database.DomainModels;

public class Order
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public AppUser AppUser { get; set; }
    public string AppUserId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedTime { get; set; }
    public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
}
