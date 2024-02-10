using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class RoleGeoZoneConfiguration : IEntityTypeConfiguration<RoleGeoZone>
    {
        public void Configure(EntityTypeBuilder<RoleGeoZone> builder)
        {
            builder.ToTable("RoleGeoZones", "HomeVisits");
            builder.HasKey(x => x.RoleGeoZoneId);

            builder.Property(x => x.RoleId).IsRequired();
            builder.Property(x => x.GeoZoneId).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.HasOne(x => x.GeoZone).WithMany().HasForeignKey(x => x.GeoZoneId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}