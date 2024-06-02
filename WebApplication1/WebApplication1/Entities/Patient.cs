namespace WebApplication1.Entities;

public class Patient
{
    public int IdPatient { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public DateOnly Birthdate { get; set; }
    public ICollection<Prescription> Prescriptions = new List<Prescription>();
}