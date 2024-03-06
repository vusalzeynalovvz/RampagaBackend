using Rampage.Database.Abstracts;

namespace Rampage.Database.DomainModels;

public class Blog : BaseEntity, IEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImagePath { get; set; } = null!;
    public DateTime CreatedTime { get; set; } 
    public BlogCategory BlogCategory { get; set; } = null!;
    public int BlogCategoryId { get; set; }
}
