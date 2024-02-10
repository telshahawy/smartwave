using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class GeoZoneConfiguration : IEntityTypeConfiguration<GeoZone>
    {
        public void Configure(EntityTypeBuilder<GeoZone> builder)
        {
            builder.ToTable("GeoZones", "HomeVisits");
            builder.HasKey(x => x.GeoZoneId);
            builder.Property(x => x.Code).IsRequired();
            builder.HasIndex(x => x.Code).IsUnique();
            builder.Property(x => x.GovernateId).IsRequired();
            builder.Property(x => x.NameAr).HasMaxLength(100).IsRequired();
            builder.Property(x => x.NameEn).HasMaxLength(100).IsRequired();
            builder.Property(x => x.KmlFilePath).HasMaxLength(1000).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false);
            builder.Property(x => x.CreatedAt).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired();
            builder.HasOne(x => x.Governate).WithMany().HasForeignKey(x => x.GovernateId);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.HasMany(x => x.TimeZones).WithOne().HasForeignKey(x => x.GeoZoneId);
        }
    }
}