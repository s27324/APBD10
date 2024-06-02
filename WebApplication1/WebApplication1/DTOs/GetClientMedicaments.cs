namespace WebApplication1.DTOs;

public class GetClientMedicaments
{
    public int IdMedicament { get; set; }
    public String Name { get; set; }
    public int? Dose { get; set; }
    public String Description { get; set; }
}