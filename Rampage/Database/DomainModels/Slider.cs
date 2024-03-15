namespace Rampage.Database.DomainModels;

public class Slider
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string ImagePath { get; set; }=null!;
    public string? Button { get; set; }
}
