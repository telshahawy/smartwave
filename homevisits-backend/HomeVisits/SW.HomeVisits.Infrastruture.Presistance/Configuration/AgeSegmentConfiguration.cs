using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class AgeSegmentConfiguration : IEntityTypeConfiguration<AgeSegment>
    {
        public void Configure(EntityTypeBuilder<AgeSegment> builder)
        {
            builder.ToTable("AgeSegments", "HomeVisits");
            builder.HasKey(x => x.AgeSegmentId);
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.AgeFromDay).IsRequired();
            builder.Property(x => x.AgeFromMonth).IsRequired();
            builder.Property(x => x.AgeFromYear).IsRequired();
            builder.Property(x => x.AgeToDay).IsRequired();
            builder.Property(x => x.AgeToMonth).IsRequired();
            builder.Property(x => x.AgeToYear).IsRequired();
            builder.Property(x => x.AgeFromInclusive).IsRequired();
            builder.Property(x => x.AgeToInclusive).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.NeedExpert).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.HasOne(x => x.Client).WithMany().HasForeignKey(x => x.ClientId);
        }
    }
}
