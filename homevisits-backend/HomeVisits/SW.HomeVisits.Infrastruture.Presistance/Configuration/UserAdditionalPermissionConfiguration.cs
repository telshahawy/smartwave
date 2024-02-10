using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class UserAdditionalPermissionConfiguration : IEntityTypeConfiguration<UserAdditionalPermission>
    {
        public void Configure(EntityTypeBuilder<UserAdditionalPermission> builder)
        {
            builder.ToTable("UserAdditionalPermissions", "HomeVisits");
            builder.HasKey(x => x.UserAdditionalPermissionId);

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.SystemPagePermissionId).IsRequired();
            builder.HasOne(x => x.SystemPagePermission).WithMany().HasForeignKey(x => x.SystemPagePermissionId);
            //builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}
