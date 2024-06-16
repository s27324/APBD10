using System.Threading;
using System.Threading.Tasks;
using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IHospitalService
{
    public Task<string> AddNewPrescriptionAsync(CancellationToken cancellationToken, PrescriptionDTO prescriptionDto);
    public Task<GetClientDTO> GetClient(int id);
}