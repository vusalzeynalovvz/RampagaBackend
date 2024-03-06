namespace Rampage.ViewModels;

public class BlogPostVM
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile Image { get; set; } = null!;

    public int BlogCategoryId { get; set; }

}
