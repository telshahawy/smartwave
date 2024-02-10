using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class ReasonConfiguration : IEntityTypeConfiguration<Reason>
    {
        public void Configure(EntityTypeBuilder<Reason> builder)
        {
            builder.ToTable("Reasons", "HomeVisits");
            builder.HasKey(x => x.ReasonId);

            builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.VisitTypeActionId).IsRequired();
            builder.Property(x => x.ReasonActionId);
            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.HasOne(x => x.VisitActionType).WithMany().HasForeignKey(x => x.VisitTypeActionId);
            builder.HasOne(x => x.ReasonAction).WithMany().HasForeignKey(x => x.ReasonActionId);
            builder.HasOne(x => x.Client).WithMany().HasForeignKey(x => x.ClientId);


        }
    }
}
