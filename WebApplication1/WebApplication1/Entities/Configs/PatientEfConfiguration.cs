using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities;

public class PatientEfConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder
            .HasKey(x => x.IdPatient)
            .HasName("Patient_pk");
        builder
            .Property(x => x.IdPatient)
            .UseIdentityColumn();
        builder
            .Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        builder
            .Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);
        builder
            .Property(x => x.Birthdate)
            .IsRequired();

        builder.ToTable(nameof(Patient));

        Patient[] patients =
        {
            new Patient
            {
                IdPatient = 1, FirstName = "Kamil", LastName = "Pilarczyk", Birthdate = new DateOnly(1990, 9, 5)
            },
            new Patient
            {
                IdPatient = 2, FirstName = "Jerzy", LastName = "Niewolnik", Birthdate = new DateOnly(1970, 5, 19)
            }
        };

        builder.HasData(patients);
    }
}