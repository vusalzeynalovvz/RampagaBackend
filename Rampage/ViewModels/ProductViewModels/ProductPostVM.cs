using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rampage.ViewModels;

public class ProductPostVM
{
    public string Name { get; set; } = null!;
    public string Barcode { get; set; } = null!;
    public IFormFile BaseImage { get; set; } = null!;
    public IFormFile BGImage { get; set; } = null!;
    public List<IFormFile> Images { get; set; } = new();
    //public Dictionary<string,string> Infos { get; set; } = new();
    [Range(0, int.MaxValue, ErrorMessage = "Miktar bir negatif değer olamaz.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public int Count { get; set; }
    public int CategoryId { get; set; }
    public int ColorId { get; set; }

}
