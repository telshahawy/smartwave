using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions", "HomeVisits");
            builder.HasKey(x => x.RolePermissionId);

            builder.Property(x => x.SystemPagePermissionId).IsRequired();
            builder.Property(x => x.RoleId).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.HasOne(x => x.SystemPagePermission).WithMany().HasForeignKey(x => x.SystemPagePermissionId);
        }
    }
}
