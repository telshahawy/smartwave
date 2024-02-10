using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class SystemPageConfiguration : IEntityTypeConfiguration<SystemPage>
    {
        public void Configure(EntityTypeBuilder<SystemPage> builder)
        {
            builder.ToTable("SystemPages", "HomeVisits");
            builder.HasKey(x => x.SystemPageId);

            builder.Property(x => x.SystemPageId).ValueGeneratedNever();
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.NameAr).HasMaxLength(250).IsRequired();
            builder.Property(x => x.NameEn).HasMaxLength(250).IsRequired();
            //builder.HasMany(x => x.SystemPagePermissions).WithOne().HasForeignKey(x => x.SystemPageId);
        }
    }
}
