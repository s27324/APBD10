using System;
using System.Collections.Generic;

namespace WebApplication1.Entities;

public class Medicament
{
    public int IdMedicament { get; set; }
    public String Name { get; set; }
    public String Description { get; set; }
    public String Type { get; set; }
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments = new List<PrescriptionMedicament>();
}