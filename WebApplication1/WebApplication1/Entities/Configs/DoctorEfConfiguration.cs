using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities;

public class DoctorEfConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder
            .HasKey(x => x.IdDoctor)
            .HasName("Doctor_pk");
        builder
            .Property(x => x.IdDoctor)
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
            .Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.ToTable(nameof(Doctor));

        Doctor[] doctors =
        {
            new Doctor { IdDoctor = 1, FirstName = "Kacper", LastName = "Alot", Email = "kacper.alot1602@gmail.com" },
            new Doctor { IdDoctor = 2, FirstName = "Dominik", LastName = "Koryncki", Email = "koryncki@gmail.com" }
        };

        builder.HasData(doctors);
    }
}