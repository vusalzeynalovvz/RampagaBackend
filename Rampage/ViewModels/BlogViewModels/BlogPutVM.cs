namespace Rampage.ViewModels;

public class BlogPutVM
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile? Image { get; set; }
    public string ImagePath { get; set; } = null!;

    public int BlogCategoryId { get; set; }

}
