using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries", "HomeVisits");
            builder.HasKey(x => x.CountryId);
            builder.Property(x => x.Code).IsRequired();
            builder.HasIndex(x => x.Code).IsUnique();
            builder.Property(x => x.MobileNumberLength).IsRequired();
            builder.Property(x => x.CountryId).ValueGeneratedNever();
            builder.Property(x => x.ClientId).IsRequired(false);
            builder.Property(x => x.CountryNameAr).HasMaxLength(100).IsRequired();
            builder.Property(x => x.CountryNameEn).HasMaxLength(100).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired(false);
            builder.Property(x => x.CreatedAt).IsRequired(false);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.HasOne(x => x.Client).WithMany().HasForeignKey(x => x.ClientId);
        }
    }
}
