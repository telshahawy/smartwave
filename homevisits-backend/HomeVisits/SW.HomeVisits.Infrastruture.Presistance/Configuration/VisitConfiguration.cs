using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class VisitConfiguration : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> builder)
        {
            builder.ToTable("Visits", "HomeVisits");
            builder.HasKey(x => x.VisitId);

            builder.Property(x => x.VisitNo).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.VisitNo).IsUnique();
            builder.Property(x => x.VisitCode).IsRequired();
            builder.HasIndex(x => x.VisitCode).IsUnique();
            builder.Property(x => x.VisitTypeId).IsRequired();
            builder.Property(x => x.VisitDate).IsRequired();
            builder.Property(x => x.PatientId).IsRequired();
            builder.Property(x => x.PatientAddressId).IsRequired();
            builder.Property(x => x.ChemistId).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.RelativeAgeSegmentId).IsRequired(false);
            builder.Property(x => x.TimeZoneGeoZoneId).IsRequired();
            builder.Property(x => x.RequiredTests);
            builder.Property(x => x.Comments);
            builder.Property(x => x.OriginVisitId).IsRequired(false);
            builder.Property(x => x.MinMinutes).IsRequired(false);
            builder.Property(x => x.MaxMinutes).IsRequired(false);
            builder.Property(x => x.SelectBy).IsRequired();
            builder.Property(x => x.VisitTime).IsRequired(false);

            builder.HasMany(x => x.Attachments).WithOne().HasForeignKey(x => x.VisitId);
            builder.HasMany(x => x.VisitStatuses).WithOne(x=>x.Visit).HasForeignKey(x => x.VisitId);
            builder.HasMany(x => x.VisitActions).WithOne(x=>x.Visit).HasForeignKey(x => x.VisitId);
            builder.HasOne(x => x.PatientAddress).WithMany().HasForeignKey(x => x.PatientAddressId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Chemist).WithMany().HasForeignKey(x => x.ChemistId).IsRequired(false);
            builder.HasOne(x => x.RelativeAgeSegment).WithMany().HasForeignKey(x => x.RelativeAgeSegmentId);
            builder.HasOne(x => x.VisitType).WithMany().HasForeignKey(x => x.VisitTypeId);
            builder.HasOne(x => x.Patient).WithMany().HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.OriginVisit).WithMany().HasForeignKey(x => x.OriginVisitId).IsRequired(false);
            builder.HasOne(x => x.timeZoneFrame).WithMany().HasForeignKey(x => x.TimeZoneGeoZoneId);

        }
    }
}
