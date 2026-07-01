namespace DoctorService.Api.Models;

public class Doctor
{
    public int DoctorId { get; set; }       // Primary Key
    public string Name { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public int Experience { get; set; }     // years of experience
    public decimal Fee { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}