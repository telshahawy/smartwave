using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications", "HomeVisits");
            builder.HasKey(x => x.NotificationId);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Reciever).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Message).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Link).HasMaxLength(250);
            builder.Property(x => x.NotificationType).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.TitleAr).IsRequired();
            builder.Property(x => x.MessageAr).IsRequired();
        }
    }
}
