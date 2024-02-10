using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class ChemistVisitOrderConfiguration : IEntityTypeConfiguration<ChemistVisitOrder>
    {
        public void Configure(EntityTypeBuilder<ChemistVisitOrder> builder)
        {
            builder.ToTable("ChemistVisitOrder", "HomeVisits");
            builder.HasKey(x => x.ChemistVisitOrderId);
            builder.Property(x => x.ChemistVisitOrderId).ValueGeneratedNever();
            builder.Property(x => x.ChemistId).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.TimeZoneFrameId).IsRequired();
            builder.Property(x => x.VisitId).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.HasOne(x => x.Visit).WithMany().HasForeignKey(x => x.VisitId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Chemist).WithMany().HasForeignKey(x => x.ChemistId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.TimeZoneFrame).WithMany().HasForeignKey(x => x.TimeZoneFrameId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
