using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients", "HomeVisits");
            builder.HasKey(x => x.PatientId);

            builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
            builder.Property(x => x.PatientNo).HasMaxLength(100).IsRequired();
            builder.Property(x => x.DOB).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.HasMany(x => x.Addresses).WithOne().HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Phones).WithOne().HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Client).WithMany().HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
