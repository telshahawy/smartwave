using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class VisitStatusConfiguration : IEntityTypeConfiguration<VisitStatus>
    {
        public void Configure(EntityTypeBuilder<VisitStatus> builder)
        {
            builder.ToTable("VisitStatus", "HomeVisits");
            builder.HasKey(x => x.VisitStatusId);
            builder.Property(x => x.VisitId).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired(false);
            builder.Property(x => x.Longitude).IsRequired(false);
            builder.Property(x => x.Latitude).IsRequired(false);
            builder.Property(x => x.DeviceSerialNumber).HasMaxLength(100).IsRequired(false);
            builder.Property(x => x.MobileBatteryPercentage).IsRequired(false);
            builder.Property(x => x.VisitActionTypeId).IsRequired();
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.ReasonId);
            builder.Property(x => x.Comments).IsRequired(false);

            builder.HasOne(x => x.VisitStatusType).WithMany().HasForeignKey(x => x.VisitStatusTypeId);
            builder.HasOne(x => x.VisitActionType).WithMany().HasForeignKey(x => x.VisitActionTypeId);
            builder.HasOne(x => x.Reason).WithMany().HasForeignKey(x => x.ReasonId);




        }
    }
}