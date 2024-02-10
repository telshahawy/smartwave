using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class ChemistTrackingLogConfiguration : IEntityTypeConfiguration<ChemistTrackingLog>
    {
        public void Configure(EntityTypeBuilder<ChemistTrackingLog> builder)
        {
            builder.ToTable("ChemistTrackingLog", "HomeVisits");
            builder.HasKey(x => x.ChemistTrackingLogId);
            builder.Property(x => x.ChemistId).IsRequired();
            builder.Property(x => x.Longitude).IsRequired();
            builder.Property(x => x.Latitude).IsRequired();
            builder.Property(x => x.DeviceSerialNumber).HasMaxLength(100).IsRequired();
            builder.Property(x => x.MobileBatteryPercentage).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(250).IsRequired();
            builder.Property(x => x.CreationDate).IsRequired();

            builder.HasOne(x => x.Chemist).WithMany().HasForeignKey(x => x.ChemistId);

        }
    }
}
