using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class VisitNotificationConfiguration : IEntityTypeConfiguration<VisitNotification>
    {
        public void Configure(EntityTypeBuilder<VisitNotification> builder)
        {
            builder.ToTable("VisitNotifications", "HomeVisits");
            builder.HasKey(x => x.VisitNotificationId);

            builder.HasOne(x => x.Notification).WithMany().HasForeignKey(x => x.NotificationId);
            builder.HasOne(x => x.Visit).WithMany().HasForeignKey(x => x.VisitId);
        }
    }
}
