using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public interface IHospitalRepository
{
    public Task<int> CheckForPatient(CancellationToken cancellationToken, PrescriptionDTO prescriptionDto);
    public List<int> ListOfMedicamentIdsInPrescription(PrescriptionDTO prescriptionDto);
    public List<Medicament> PrescribedMedicamentInDb(PrescriptionDTO prescriptionDto, List<int> medicamentsPrescribed);
    public bool DueDateHigherThanDate(PrescriptionDTO prescriptionDto);

    public Task<bool> CheckForDoctor(PrescriptionDTO prescriptionDto, CancellationToken cancellationToken);
    public Task<int> AddNewPrescriptionAsync(CancellationToken cancellationToken, PrescriptionDTO prescriptionDto);

    public Task<GetClientDTO> GetClient(int id);
}