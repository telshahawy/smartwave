using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class VisitStatusTypeConfiguration : IEntityTypeConfiguration<VisitStatusType>
    {
        public void Configure(EntityTypeBuilder<VisitStatusType> builder)
        {
            builder.ToTable("VisitStatusTypes", "HomeVisits");
            builder.HasKey(x => x.VisitStatusTypeId);
            builder.Property(x => x.VisitStatusTypeId).ValueGeneratedNever();
            builder.Property(x => x.StatusNameAr).HasMaxLength(100).IsRequired();
            builder.Property(x => x.StatusNameEn).HasMaxLength(100).IsRequired();
        }
    }
}
