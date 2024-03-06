using System.ComponentModel.DataAnnotations;

namespace Rampage.ViewModels;

public class CategoryPostVM
{


    [Required(ErrorMessage = "Kategori adı gereklidir.")]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "Kategori adı en az 3 en fazla 128 karakter olmalıdır.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Resim gereklidir.")]
    public IFormFile Image { get; set; } = null!;

    [Required(ErrorMessage = "Arkaplan resmi  gereklidir.")]
    public IFormFile BGImage { get; set; } = null!;

    public int? ParentCategoryId { get; set; }
}
