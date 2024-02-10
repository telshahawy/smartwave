using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class UserGeoZoneConfiguration : IEntityTypeConfiguration<UserGeoZone>
    {
        public void Configure(EntityTypeBuilder<UserGeoZone> builder)
        {
            builder.ToTable("UserGeoZones", "HomeVisits");
            builder.HasKey(x => x.UserGeoZoneId);

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.GeoZoneId).IsRequired();
        }
    }
}