using Rampage.Database.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace Rampage.Database.DomainModels;

public class Category : BaseEntity, IEntity
{
    [Required(ErrorMessage = "Kategori adı gereklidir.")]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "Kategori adı en az 3 en fazla 128 karakter olmalıdır.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Resim gereklidir.")]
    public string ImagePath { get; set; } = null!;

    [Required(ErrorMessage = "Arkaplan resmi  gereklidir.")]
    public string BGImagePath { get; set; } = null!;

    public Category? ParentCategory { get; set; }
    public int? ParentCategoryId { get; set; }
    public ICollection<Category> ChildCategories { get; set; } = new List<Category>();
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
