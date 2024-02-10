using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class SystemPagePermissionConfiguration : IEntityTypeConfiguration<SystemPagePermission>
    {
        public void Configure(EntityTypeBuilder<SystemPagePermission> builder)
        {
            builder.ToTable("SystemPagePermissions", "HomeVisits");
            builder.HasKey(x => x.SystemPagePermissionId);

            builder.Property(x => x.SystemPagePermissionId).ValueGeneratedNever();
            builder.Property(x => x.NameAr).HasMaxLength(250).IsRequired(false);
            builder.Property(x => x.NameEn).HasMaxLength(250).IsRequired(false);
        }
    }
}
