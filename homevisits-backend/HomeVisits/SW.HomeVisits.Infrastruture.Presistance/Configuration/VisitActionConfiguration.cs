using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class VisitActionConfiguration : IEntityTypeConfiguration<VisitAction>
    {
        public void Configure(EntityTypeBuilder<VisitAction> builder)
        {
            builder.ToTable("VisitActions", "HomeVisits");
            builder.HasKey(x => x.VisitActionId);
            builder.Property(x => x.VisitId).IsRequired();
            builder.Property(x => x.VisitActionTypeId).IsRequired();
            builder.Property(x => x.CreationDate).IsRequired();

            builder.Property(x => x.ReasonId);
            builder.Property(x => x.Comments);
            builder.HasOne(x => x.Reason).WithMany().HasForeignKey(x => x.ReasonId);
        }
    }
}
