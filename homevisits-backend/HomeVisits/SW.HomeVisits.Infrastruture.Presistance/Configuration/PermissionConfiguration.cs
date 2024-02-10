using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions", "HomeVisits");
            builder.HasKey(x => x.PermissionId);
            builder.Property(x => x.PermissionId).ValueGeneratedNever();
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.NameAr).HasMaxLength(250).IsRequired();
            builder.Property(x => x.NameEn).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Position).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired();
            //builder.Property(x => x.SystemPageId).IsRequired();

            //builder.HasMany(x => x.PermissionUsages).WithOne().HasForeignKey(x => x.PermissionId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
