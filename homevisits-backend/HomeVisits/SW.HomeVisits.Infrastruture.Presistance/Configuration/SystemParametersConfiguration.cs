using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.Framework.Extensions;
using SW.HomeVisits.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    class SystemParametersConfiguration : IEntityTypeConfiguration<SystemParameter>
    {
        public void Configure(EntityTypeBuilder<SystemParameter> builder)
        {
            builder.ToTable("SystemParameters", "HomeVisits");
            builder.HasKey(x => x.ClientId);

            builder.Property(x => x.CallCenterNumber).HasMaxLength(50).IsNullOrDefault();
            builder.Property(x => x.DefaultCountryId).IsNullOrDefault();
            builder.Property(x => x.DefaultGovernorateId).IsNullOrDefault();

            builder.Property(x => x.CreateBy).IsNullOrDefault();
            builder.Property(x => x.EstimatedVisitDurationInMin).IsRequired();
            builder.Property(x => x.IsOptimizezonebefore).IsRequired();
            builder.Property(x => x.IsSendPatientTimeConfirmation).IsRequired();
            builder.Property(x => x.NextReserveHomevisitInDay).IsRequired();
            builder.Property(x => x.OptimizezonebeforeInMin).IsRequired();
            builder.Property(x => x.RoutingSlotDurationInMin).IsRequired();
            builder.Property(x => x.VisitApprovalBy).HasMaxLength(50).IsRequired();
            builder.Property(x => x.VisitCancelBy).HasMaxLength(50).IsRequired();
            builder.Property(x => x.WhatsappBusinessLink);
            builder.Property(x => x.PrecautionsFile);
            builder.Property(x => x.RoutingSlotDurationInMin).IsRequired();
          
            builder.HasOne(x => x.Client).WithOne().HasForeignKey<Client>(x => x.ClientId);
            builder.HasOne(x => x.Country).WithMany().HasForeignKey(x => x.DefaultCountryId);
            builder.HasOne(x => x.Governate).WithMany().HasForeignKey(x => x.DefaultGovernorateId);

        }
    }
}
