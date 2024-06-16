using System;
using System.Collections.Generic;

namespace WebApplication1.DTOs;

public class GetClientPrescriptionsDTO
{
    public int IdPrescription { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public List<GetClientMedicaments> Medicaments { get; set; }
    public GetClientDoctor Doctor { get; set; }
}