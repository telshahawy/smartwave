using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class VisitTypeConfiguration : IEntityTypeConfiguration<VisitType>
    {
        public void Configure(EntityTypeBuilder<VisitType> builder)
        {
            builder.ToTable("VisitType", "HomeVisits");
            builder.HasKey(x => x.VisitTypeId);
            builder.Property(x => x.VisitTypeId).ValueGeneratedNever();
            builder.Property(x => x.TypeNameAr).HasMaxLength(100).IsRequired();
            builder.Property(x => x.TypeNameEn).HasMaxLength(100).IsRequired();

        }
    }
} 