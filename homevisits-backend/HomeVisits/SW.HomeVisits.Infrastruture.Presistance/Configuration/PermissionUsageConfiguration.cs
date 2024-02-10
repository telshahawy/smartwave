using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class PermissionUsageConfiguration : IEntityTypeConfiguration<PermissionUsage>
    {
        public void Configure(EntityTypeBuilder<PermissionUsage> builder)
        {
            builder.ToTable("PermissionUsages", "HomeVisits");
            builder.HasKey(x => x.PermissionUsageId);
            builder.Property(x => x.PermissionUsageId).ValueGeneratedNever();

            builder.Property(x => x.PermissionId).IsRequired();
            builder.Property(x => x.Usage).IsRequired();
        }
    }
}
