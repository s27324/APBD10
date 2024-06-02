using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities;

public class PrescriptionEfConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder
            .HasKey(x => x.IdPrescription)
            .HasName("Prescription_pl");
        builder
            .Property(x => x.IdPrescription)
            .UseIdentityColumn();
        builder
            .Property(x => x.Date)
            .IsRequired();
        builder
            .Property(x => x.DueDate)
            .IsRequired();
        builder
            .HasOne(x => x.Patient)
            .WithMany(x => x.Prescriptions)
            .HasForeignKey(x => x.IdPatient)
            .HasConstraintName("Prescription_Patient")
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(x => x.Doctor)
            .WithMany(x => x.Prescriptions)
            .HasForeignKey(x => x.IdDoctor)
            .HasConstraintName("Prescription_Dcotor")
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(nameof(Prescription));

        Prescription[] prescriptions =
        {
            new Prescription
            {
                IdPrescription = 1, Date = new DateOnly(2023, 9, 10), DueDate = new DateOnly(2023, 10, 10),
                IdPatient = 1, IdDoctor = 1
            },
            new Prescription
            {
                IdPrescription = 2, Date = new DateOnly(2024, 6, 1), DueDate = new DateOnly(2024, 7, 11), IdPatient = 2,
                IdDoctor = 2
            }
        };

        builder.HasData(prescriptions);
    }
}