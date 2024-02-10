using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles", "HomeVisits");
            builder.HasKey(x => x.RoleId);

            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.ClientId).IsRequired(false);
            builder.Property(x => x.NameAr).HasMaxLength(250).IsRequired();
            builder.Property(x => x.NameEn).HasMaxLength(250).IsRequired(false);
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.DefaultPageId).IsRequired();
            builder.HasMany(x => x.RolePermissions).WithOne().HasForeignKey(x => x.RoleId);
            builder.HasOne(x => x.Client).WithMany().HasForeignKey(x => x.ClientId);
            builder.HasOne(x => x.DefaultPage).WithMany().HasForeignKey(x => x.DefaultPageId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.GeoZones).WithOne().HasForeignKey(x => x.RoleId);

        }
    }
}