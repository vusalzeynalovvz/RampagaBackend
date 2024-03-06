using System.ComponentModel.DataAnnotations;

namespace Rampage.ViewModels;

public class CategoryPutVM
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kategori adı gereklidir.")]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "Kategori adı en az 3 en fazla 128 karakter olmalıdır.")]
    public string Name { get; set; } = null!;

    public IFormFile? Image { get; set; } = null!;

    public string? ImagePath { get; set; }

    public IFormFile? BGImage { get; set; } = null!;
    public string? BGImagePath { get; set; }

    public int? ParentCategoryId { get; set; }
}
