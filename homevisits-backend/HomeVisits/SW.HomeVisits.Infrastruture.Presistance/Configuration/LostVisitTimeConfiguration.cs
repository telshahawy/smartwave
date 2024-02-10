using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class LostVisitTimeConfiguration : IEntityTypeConfiguration<LostVisitTime>
    {
        public void Configure(EntityTypeBuilder<LostVisitTime> builder)
        {
            builder.ToTable("LostVisitTime", "HomeVisits");
            builder.HasKey(x => x.LostVisitTimeId);
            builder.Property(x => x.VisitId).IsRequired();
            builder.Property(x => x.LostTime).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired();

            builder.HasOne(x => x.Visit).WithMany().HasForeignKey(x => x.VisitId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}