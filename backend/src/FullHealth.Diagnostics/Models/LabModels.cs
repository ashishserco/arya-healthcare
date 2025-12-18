namespace FullHealth.Diagnostics.Models;

public class LabPackage
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // e.g., "Popular", "Women Health", "Condition Based"
    public decimal Price { get; set; }
    public decimal DiscountPrice { get; set; }
    public string Description { get; set; } = string.Empty;
    public int TestsIncluded { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}

public class LabTest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool FastingRequired { get; set; } = false;
}
