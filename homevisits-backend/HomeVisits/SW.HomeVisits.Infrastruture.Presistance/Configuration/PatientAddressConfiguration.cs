using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class PatientAddressConfiguration : IEntityTypeConfiguration<PatientAddress>
    {
        public void Configure(EntityTypeBuilder<PatientAddress> builder)
        {
            builder.ToTable("PatientAddress", "HomeVisits");
            builder.HasKey(x => x.PatientAddressId);
            builder.Property(x => x.Code).IsRequired();
            builder.HasIndex(x => x.Code).IsUnique();
            builder.Property(x => x.street).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Latitude).IsRequired(false);
            builder.Property(x => x.Longitude).IsRequired(false);
            builder.Property(x => x.LocationUrl).IsRequired(false);
            builder.Property(x => x.Floor).IsRequired();
            builder.Property(x => x.Flat).IsRequired();
            builder.Property(x => x.PatientId).IsRequired();
            builder.Property(x => x.GeoZoneId).IsRequired();
            builder.Property(x => x.Building).HasMaxLength(250).IsRequired();
            builder.Property(x => x.AdditionalInfo).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.IsConfirmed).IsRequired();
            builder.Property(x => x.CreateBy).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.ConfirmedBy).IsRequired(false);
            builder.Property(x => x.ConfirmedAt).IsRequired(false);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.HasOne(x => x.GeoZone).WithMany().HasForeignKey(x => x.GeoZoneId);
        }
    }
}
