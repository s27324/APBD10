namespace WebApplication1.DTOs;

public class GetClientDTO
{
    public int IdPatient { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public DateOnly Birthdate { get; set; }
    public List<GetClientPrescriptionsDTO> Prescriptions { get; set; }
}