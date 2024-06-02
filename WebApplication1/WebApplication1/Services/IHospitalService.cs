using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IHospitalService
{
    public Task<string> AddNewPrescriptionAsync(CancellationToken cancellationToken, PrescriptionDTO prescriptionDto);
}