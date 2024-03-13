namespace Rampage.Database.DomainModels;

public class BasketItem
{
    public int Id { get; set; }
    public string AppUserId { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
    public int ProductId { get; set; }
    public Product Product{ get; set; }=null!;
    public int Count { get; set; }
}