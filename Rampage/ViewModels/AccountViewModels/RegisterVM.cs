using System.ComponentModel.DataAnnotations;

namespace Rampage.ViewModels;

public class RegisterVM
{
    [Required(ErrorMessage = "Adı ve Soyadı alanı zorunludur.")]
    [Display(Name = "Adı ve Soyadı")]
    public string FullName { get; set; } = null!;

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

    [Required(ErrorMessage = "Parola Onayı alanı zorunludur.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Parolalar uyuşmuyor.")]
    [MinLength(6, ErrorMessage = "Parola en az 6 karakter olmalıdır.")]
    [Display(Name = "Parola Onayı")]
    public string ConfirmPassword { get; set; } = null!;
}
