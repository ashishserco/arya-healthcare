namespace FullHealth.Pharmacy.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal DiscountPrice { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool PrescriptionRequired { get; set; } = false;
    public string Description { get; set; } = string.Empty;
}
