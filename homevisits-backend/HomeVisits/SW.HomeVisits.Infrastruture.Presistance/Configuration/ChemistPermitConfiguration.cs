using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class ChemistPermitConfiguration : IEntityTypeConfiguration<ChemistPermit>
    {
        public void Configure(EntityTypeBuilder<ChemistPermit> builder)
        {
            builder.ToTable("ChemistPermits", "HomeVisits");
            builder.HasKey(x => x.ChemistPermitId);

            builder.Property(x => x.ChemistId).IsRequired();
            builder.Property(x => x.PermitDate).IsRequired();
            builder.Property(x => x.StartTime).IsRequired();
            builder.Property(x => x.EndTime).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}