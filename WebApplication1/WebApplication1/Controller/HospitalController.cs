using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controller;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClient(int id)
    {
        return Ok(await _hospitalService.GetClient(id));
    }
}