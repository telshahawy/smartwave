using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;
namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class ChemistScheduleDayConfiguration : IEntityTypeConfiguration<ChemistScheduleDay>
    {
        public void Configure(EntityTypeBuilder<ChemistScheduleDay> builder)
        {
            builder.ToTable("ChemistScheduleDays", "HomeVisits");
            builder.HasKey(x => x.ChemistScheduleDayId);

            builder.Property(x => x.ChemistScheduleId).IsRequired();
            builder.Property(x => x.Day).IsRequired();
            builder.Property(x => x.StartTime).IsRequired();
            builder.Property(x => x.EndTime).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}
