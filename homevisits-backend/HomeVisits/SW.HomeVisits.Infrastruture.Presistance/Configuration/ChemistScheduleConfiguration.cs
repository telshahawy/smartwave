using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;
namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class ChemistScheduleConfiguration : IEntityTypeConfiguration<ChemistSchedule>
    {
        public void Configure(EntityTypeBuilder<ChemistSchedule> builder)
        {
            builder.ToTable("ChemistSchedule", "HomeVisits");
            builder.HasKey(x => x.ChemistScheduleId);
            builder.Property(x => x.ChemistAssignedGeoZoneId).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.StartLatitude).IsRequired();
            builder.Property(x => x.StartLangitude).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.HasMany(x => x.ScheduleDays).WithOne().HasForeignKey(x => x.ChemistScheduleId);

        }
    }
}
