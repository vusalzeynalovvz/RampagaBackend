namespace Rampage.ViewModels;

public class SliderPostVM
{
    public string Name { get; set; }= null!;    
    public string Subject { get; set; }= null!;    
    public string Title { get; set; }= null!;    
    public IFormFile Image { get; set; }= null!;    
    public string? Button { get; set; } 
}




public class SliderPutVM
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? ImagePath { get; set; }
    public IFormFile? Image { get; set; } = null!;
    public string? Button { get; set; }
}