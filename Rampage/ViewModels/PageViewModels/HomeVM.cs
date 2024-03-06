using Rampage.Database.DomainModels;

namespace Rampage.ViewModels;

public class HomeVM
{
    public List<Product> PopularProducts { get; set; } = new();
    public List<Product> SpecialProducts { get; set; } = new();
    public List<Product> NewProducts { get; set; } = new();
    public List<Category> Categories { get; set; } = new();

}


public class BlogVM
{
    public List<Blog> Blogs { get; set; } = new();
    public List<BlogCategory> BlogCategories { get; set; } = new();
}