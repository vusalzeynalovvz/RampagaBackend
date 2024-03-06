using System.ComponentModel.DataAnnotations;

namespace Rampage.ViewModels;

public class ColorPutVM
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Renk adı gereklidir.")]
    [StringLength(128, MinimumLength = 2, ErrorMessage = "Renk adı en az 3 en fazla 128 karakter olmalıdır.")]
    public string Name { get; set; } = null!;
}
