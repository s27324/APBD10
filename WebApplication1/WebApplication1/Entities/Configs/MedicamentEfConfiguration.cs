using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities;

public class MedicamentEfConfiguration : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        builder
            .HasKey(x => x.IdMedicament)
            .HasName("Medicament_pk");
        builder
            .Property(x => x.IdMedicament)
            .UseIdentityColumn();
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder
            .Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(100);
        builder
            .Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(100);

        builder.ToTable(nameof(Medicament));

        Medicament[] medicaments =
        {
            new Medicament { IdMedicament = 1, Name = "one", Description = "oneoneone", Type = "one" },
            new Medicament { IdMedicament = 2, Name = "two", Description = "two", Type = "two" }
        };
        builder.HasData(medicaments);
    }
}