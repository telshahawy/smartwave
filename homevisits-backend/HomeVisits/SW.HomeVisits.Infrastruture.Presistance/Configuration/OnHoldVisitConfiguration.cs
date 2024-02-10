using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;
namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class OnHoldVisitConfiguration : IEntityTypeConfiguration<OnHoldVisit>
    {
        public void Configure(EntityTypeBuilder<OnHoldVisit> builder)
        {
            builder.ToTable("OnHoldVisits", "HomeVisits");
            builder.HasKey(x => x.OnHoldVisitId);

            builder.Property(x => x.ChemistId).IsRequired(false);
            builder.Property(x => x.IsCanceled).IsRequired();
            builder.Property(x => x.TimeZoneFrameId).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.DeviceSerialNo).IsRequired(false);
            builder.Property(x => x.NoOfPatients).IsRequired();
           // builder.HasOne(x => x.Chemist).WithMany().HasForeignKey(x => x.ChemistId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.TimeZoneFrame).WithMany().HasForeignKey(x => x.TimeZoneFrameId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
