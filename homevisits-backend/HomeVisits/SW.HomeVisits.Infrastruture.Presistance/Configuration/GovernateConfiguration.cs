using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class GovernateConfiguration : IEntityTypeConfiguration<Governate>
    {
        public void Configure(EntityTypeBuilder<Governate> builder)
        {
            builder.ToTable("Governats", "HomeVisits");
            builder.HasKey(x => x.GovernateId);
            builder.Property(x => x.GoverNameAr).HasMaxLength(100).IsRequired();
            builder.Property(x => x.GoverNameEn).HasMaxLength(100).IsRequired();
            builder.Property(x => x.CountryId).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired(false);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.HasOne(x => x.Country).WithMany().HasForeignKey(x => x.CountryId);
            builder.Property(x => x.Code).IsRequired();
            builder.HasIndex(x => x.Code).IsUnique();
            builder.Property(x => x.CustomerServiceEmail).IsRequired();


        }
    }
}
