using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class ReasonActionConfiguration : IEntityTypeConfiguration<ReasonAction>
    {
        public void Configure(EntityTypeBuilder<ReasonAction> builder)
        {
            builder.ToTable("ReasonActions", "HomeVisits");
            builder.HasKey(x => x.ReasonActionId);

            builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
        }
    }
}
