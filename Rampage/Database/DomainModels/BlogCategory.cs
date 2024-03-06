using Rampage.Database.Abstracts;

namespace Rampage.Database.DomainModels;

public class BlogCategory : BaseEntity, IEntity
{
    public string Name { get; set; } = null!;
    public List<Blog> Blogs { get; set; } = new();
}
