using Rampage.Database.DomainModels;

namespace Rampage.ViewModels;

public class HomeVM
{
    public List<Product> PopularProducts { get; set; } = new();
    public List<Product> SpecialProducts { get; set; } = new();
    public List<Product> NewProducts { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public List<Category> SpecialCategories { get; set; } = new();

}


public class BlogVM
{
    public List<Blog> Blogs { get; set; } = new();
    public List<Blog> RecentBlogs { get; set; } = new();
    public List<BlogCategory> BlogCategories { get; set; } = new();
}




public class ShopVM
{
    public List<Product> Products  { get; set; } = new();
    public Category? Category  { get; set; }
    public Category? ChildCategory  { get; set; }
    public List<Category> Categories { get; set; } = new();
    public List<Color> Colors { get; set; } = new();
    public Color? Color { get; set; }
}