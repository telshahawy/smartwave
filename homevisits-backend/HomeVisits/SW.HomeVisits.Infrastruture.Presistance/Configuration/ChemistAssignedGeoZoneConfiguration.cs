using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;
namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class ChemistAssignedGeoZoneConfiguration : IEntityTypeConfiguration<ChemistAssignedGeoZone>
    {
        public void Configure(EntityTypeBuilder<ChemistAssignedGeoZone> builder)
        {
            builder.ToTable("ChemistAssignedGeoZones", "HomeVisits");
            builder.HasKey(x => x.ChemistAssignedGeoZoneId);
            builder.Property(x => x.ChemistId).IsRequired();
            builder.Property(x => x.GeoZoneId).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.HasOne(x => x.GeoZone).WithMany().HasForeignKey(x => x.GeoZoneId);
        }
    }
}
