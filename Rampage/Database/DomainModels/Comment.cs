using System.ComponentModel.DataAnnotations;

namespace Rampage.Database.DomainModels;

public class Comment
{
    public int Id { get; set; }
    [Range(0, 5)]
    public int Rating { get; set; }
    public string Title { get; set; } = null!;
    public AppUser AppUser { get; set; }=null!;
    public string AppUserId { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public int ProductId { get; set; }
    public DateTime CreatedTime { get; set; }
}
