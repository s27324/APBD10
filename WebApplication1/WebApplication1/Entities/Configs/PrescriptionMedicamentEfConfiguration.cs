using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities;

public class PrescriptionMedicamentEfConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
{
    public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
    {
        builder
            .HasKey(x => new { x.IdMedicament, x.IdPrescription })
            .HasName("PrescriptionMedicament_pk");

        builder
            .Property(x => x.Dose)
            .IsRequired(false);
        builder
            .Property(x => x.Details)
            .IsRequired()
            .HasMaxLength(100);
        builder
            .HasOne(x => x.Medicament)
            .WithMany(x => x.PrescriptionMedicaments)
            .HasForeignKey(x => x.IdMedicament)
            .HasConstraintName("PrescriptionMedicament_Medicament")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(x => x.Prescription)
            .WithMany(x => x.PrescriptionMedicaments)
            .HasForeignKey(x => x.IdPrescription)
            .HasConstraintName("PrescriptionMedicament_Prescription")
            .OnDelete(DeleteBehavior.Restrict);

        PrescriptionMedicament[] prescriptionMedicaments =
        {
            new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 1, Dose = 15, Details = "neuroleptyki" },
            new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 2, Dose = 3, Details = "rutinoscorbin" }
        };
        
        builder.HasData(prescriptionMedicaments);
    }
}