using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class UserDeviceConfiguration : IEntityTypeConfiguration<UserDevice>
    {
        public void Configure(EntityTypeBuilder<UserDevice> builder)
        {
            builder.ToTable("UserDevices", "HomeVisits");
            builder.HasKey(x => x.UserDeviceId);
            builder.Property(x => x.DeviceSerialNumber).HasMaxLength(100).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.FireBaseDeviceToken).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
