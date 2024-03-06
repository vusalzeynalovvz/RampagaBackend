using System.ComponentModel.DataAnnotations;

namespace Rampage.ViewModels;

public class LoginVM
{
    [Required(ErrorMessage = "Email alanı zorunludur.")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Geçersiz email adresi.")]
    [MaxLength(256, ErrorMessage = "Email maksimum 256 karakter olmalıdır.")]
    [MinLength(8, ErrorMessage = "Email en az 8 karakter olmalıdır.")]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Parola alanı zorunludur.")]
    [MinLength(6, ErrorMessage = "Parola en az 6 karakter olmalıdır.")]
    [DataType(DataType.Password, ErrorMessage = "Geçersiz parola.")]
    [Display(Name = "Parola")]
    public string Password { get; set; } = null!;
}
