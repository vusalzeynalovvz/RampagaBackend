using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rampage.Database.DomainModels;

namespace Rampage.Database;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }


    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductImage> ProductImages { get; set; } = null!;
    public DbSet<ProductInfo> ProductInfos { get; set; } = null!;
    public DbSet<Color> Colors { get; set; } = null!;
    public DbSet<Setting> Settings { get; set; } = null!;
    public DbSet<BlogCategory> BlogCategories { get; set; } = null!;
    public DbSet<Blog> Blogs  { get; set; } = null!;
    public DbSet<BasketItem> BasketItems  { get; set; } = null!;
}
