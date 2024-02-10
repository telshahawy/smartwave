using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients", "HomeVisits");
            builder.HasKey(x => x.ClientId);

            builder.Property(x => x.ClientCode).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ClientName).HasMaxLength(250).IsRequired();
            builder.Property(x => x.CountryId).IsRequired();
            builder.Property(x => x.URLName).HasMaxLength(250).IsRequired();
            builder.Property(x => x.DisplayName).HasMaxLength(250).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.Logo).HasMaxLength(250).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.HasOne(x => x.Country).WithMany().HasForeignKey(x => x.CountryId);
        }
    }
}