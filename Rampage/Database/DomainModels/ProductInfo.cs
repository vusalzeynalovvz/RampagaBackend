using Rampage.Database.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace Rampage.Database.DomainModels;

public class ProductInfo : BaseEntity, IEntity
{
    public Product Product { get; set; } = null!;

    [Required(ErrorMessage = "Ürün ID gereklidir.")]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Anahtar gereklidir.")]
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Anahtar en az 1 en fazla 128 karakter olmalıdır.")]
    public string Key { get; set; } = null!;

    [Required(ErrorMessage = "Değer gereklidir.")]
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Değer en az 1 en fazla 128 karakter olmalıdır.")]
    public string Value { get; set; } = null!;
}
