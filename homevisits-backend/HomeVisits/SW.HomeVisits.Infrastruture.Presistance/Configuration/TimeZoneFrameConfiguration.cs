using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class TimeZoneFrameConfiguration : IEntityTypeConfiguration<TimeZoneFrame>
    {
        public void Configure(EntityTypeBuilder<TimeZoneFrame> builder)
        {
            builder.ToTable("TimeZoneFrames", "HomeVisits");
            builder.HasKey(x => x.TimeZoneFrameId);
            builder.Property(x => x.TimeZoneFrameId).ValueGeneratedNever();
            builder.Property(x => x.GeoZoneId).IsRequired();
            builder.Property(x => x.NameAr).HasMaxLength(100).IsRequired();
            builder.Property(x => x.NameEN).HasMaxLength(100).IsRequired();
            builder.Property(x => x.VisitsNoQouta).IsRequired();
            builder.Property(x => x.BranchDispatch).IsRequired();
            builder.Property(x => x.StartTime).IsRequired();
            builder.Property(x => x.EndTime).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}
