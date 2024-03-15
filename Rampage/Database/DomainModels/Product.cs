using Rampage.Database.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rampage.Database.DomainModels;

public class Product : BaseEntity, IEntity
{
    [Required(ErrorMessage = "Ürün adı gereklidir.")]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "Ürün adı en az 3 en fazla 128 karakter olmalıdır.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Fiyat gereklidir.")]
    [Range(0, double.MaxValue, ErrorMessage = "Fiyat bir negatif değer olamaz.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Miktar gereklidir.")]
    [Range(0, int.MaxValue, ErrorMessage = "Miktar bir negatif değer olamaz.")]
    [Column(TypeName = "decimal(18,2)")]
    public int Count { get; set; }

    [Required(ErrorMessage = "Barkod gereklidir.")]
    public string Barcode { get; set; } = null!;

    [Required(ErrorMessage = "Ana resim  gereklidir.")]
    public string BaseImagePath { get; set; } = null!;

    [Required(ErrorMessage = "Arkaplan resmi gereklidir.")]
    public string BGImagePath { get; set; } = null!;


    public Category Category { get; set; } = null!;
    [Required(ErrorMessage = "Kategori gereklidir.")]
    public int CategoryId { get; set; }

    public Color Color { get; set; } = null!;
    [Required(ErrorMessage = "Renk gereklidir.")]
    public int ColorId { get; set; }

    public int Rating { get; set; } = 5;
    public int SalesCount { get; set; } = 0;

    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    public ICollection<ProductInfo> ProductInfos { get; set; } = new List<ProductInfo>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

}
