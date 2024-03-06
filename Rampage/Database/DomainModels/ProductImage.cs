using Rampage.Database.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace Rampage.Database.DomainModels;

public class ProductImage : BaseEntity, IEntity
{
    public Product Product { get; set; } = null!;

    [Required(ErrorMessage = "Ürün ID gereklidir.")]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Resim gereklidir.")]
    public string ImagePath { get; set; } = null!;
}
