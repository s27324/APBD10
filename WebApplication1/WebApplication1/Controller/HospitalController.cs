using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controller;

[Route("api/[controller]")]
[ApiController]
public class HospitalController : ControllerBase
{
    private readonly IHospitalService _hospitalService;

    public HospitalController(IHospitalService hospitalService)
    {
        _hospitalService = hospitalService;
    }

    [HttpPost("addPrescription")]
    public async Task<IActionResult> PostNewPrescription(CancellationToken cancellationToken,
        PrescriptionDTO prescriptionDto)
    {
        string message = await _hospitalService.AddNewPrescriptionAsync(cancellationToken, prescriptionDto);
        if (message.StartsWith("Error"))
        {
            return NotFound(message);
        }

        return Ok(message);
    }
}