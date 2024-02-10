
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class ChemistConfiguration : IEntityTypeConfiguration<Chemist>
    {
        public void Configure(EntityTypeBuilder<Chemist> builder)
        {
            builder.ToTable("Chemists", "HomeVisits");
            builder.HasKey(x => x.ChemistId);

            builder.Property(x => x.Code).IsRequired();

            builder.Property(x => x.DOB).IsRequired();
            //builder.Property(x => x.MobileNumber).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ExpertChemist).IsRequired();
            builder.Property(x => x.JoinDate).IsRequired();
            builder.HasMany(x => x.ChemistsGeoZones).WithOne().HasForeignKey(x => x.ChemistId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.ChemistPermits).WithOne().HasForeignKey(x => x.ChemistId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
