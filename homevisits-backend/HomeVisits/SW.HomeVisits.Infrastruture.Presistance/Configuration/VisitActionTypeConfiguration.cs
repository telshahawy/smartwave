using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class VisitActionTypeConfiguration : IEntityTypeConfiguration<VisitActionType>
    {
        public void Configure(EntityTypeBuilder<VisitActionType> builder)
        {
            builder.ToTable("VisitActionType", "HomeVisits");
            builder.HasKey(x => x.VisitActionTypeId);
            builder.Property(x => x.ActionNameAr).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ActionNameEn).HasMaxLength(100).IsRequired();
        }
    }
}
