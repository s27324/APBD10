using WebApplication1.DTOs;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class HospitalService : IHospitalService
{
    private readonly IHospitalRepository _hospitalRepository;

    public HospitalService(IHospitalRepository hospitalRepository)
    {
        _hospitalRepository = hospitalRepository;
    }

    public async Task<string> AddNewPrescriptionAsync(CancellationToken cancellationToken, PrescriptionDTO prescriptionDto)
    {
        int code = await _hospitalRepository.AddNewPrescriptionAsync(cancellationToken, prescriptionDto);

        switch (code)
        {
            case -1:
                return "Error: Some medicaments were not found.";
            case -2:
                return "Error: Too many medicaments prescribed.";
            case -3:
                return "Error: Prescription have wrong dates. DueDate should be higher than or equal Date.";
            case -4:
                return "Error: Doctor not found.";
            default:
                return "Prescription successfully added.";
        }
        
    }

    public Task<GetClientDTO> GetClient(int id)
    {
        return _hospitalRepository.GetClient(id);
    }
}