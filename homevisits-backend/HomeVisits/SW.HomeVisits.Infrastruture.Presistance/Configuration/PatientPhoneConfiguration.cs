using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;
namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class PatientPhoneConfiguration : IEntityTypeConfiguration<PatientPhone>
    {
        public void Configure(EntityTypeBuilder<PatientPhone> builder)
        {
            builder.ToTable("PatientPhones", "HomeVisits");
            builder.HasKey(x => x.PatientPhoneId);

            builder.Property(x => x.PatientId).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(x => x.CreateBy).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}
