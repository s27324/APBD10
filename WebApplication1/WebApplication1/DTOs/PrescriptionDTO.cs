namespace WebApplication1.DTOs;

public class PrescriptionDTO
{
    public PatientDTO patient { get; set; }
    public List<MedicamentDTO> medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
}