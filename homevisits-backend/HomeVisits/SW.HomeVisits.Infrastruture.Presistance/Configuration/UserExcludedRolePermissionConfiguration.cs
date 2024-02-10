using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class UserExcludedRolePermissionConfiguration : IEntityTypeConfiguration<UserExcludedRolePermission>
    {
        public void Configure(EntityTypeBuilder<UserExcludedRolePermission> builder)
        {
            builder.ToTable("UserExcludedRolePermissions", "HomeVisits");
            builder.HasKey(x => x.UserExcludedRolePermissionId);

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.RoleId).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.SystemPagePermissionId).IsRequired();
            //builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId);
            builder.HasOne(x => x.SystemPagePermission).WithMany().HasForeignKey(x => x.SystemPagePermissionId);
        }
    }
}
