namespace FullHealth.Provider.Models;

public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string Qualification { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public decimal ConsultationFee { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string Languages { get; set; } = string.Empty; // Comma separated for simplicity
    public bool IsAvailable { get; set; } = true;
}
