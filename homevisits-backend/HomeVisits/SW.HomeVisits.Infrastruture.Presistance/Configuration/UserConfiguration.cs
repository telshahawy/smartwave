using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Infrastruture.Presistance.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "HomeVisits");
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
            builder.Property(x => x.UserType).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Gender).IsRequired(false);
            builder.Property(x => x.NormalizedUserName).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.NormalizedEmail).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.EmailConfirmed).IsRequired();
            builder.Property(x => x.PasswordHash).HasMaxLength(500).IsRequired();
            builder.Property(x => x.SecurityStamp).HasMaxLength(500).IsRequired();
            builder.Property(x => x.ConcurrencyStamp).HasMaxLength(500).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(x => x.PhoneNumberConfirmed).IsRequired();
            builder.Property(x => x.TwoFactorEnabled).IsRequired();
            builder.Property(x => x.LockoutEnd).IsRequired(false);
            builder.Property(x => x.LockoutEnabled).IsRequired();
            builder.Property(x => x.AccessFailedCount).IsRequired();
            builder.Property(x => x.BirthDate).IsRequired(false);
            builder.Property(x => x.PersonalPhoto).IsRequired(false);
            builder.Property(x => x.UserCreationTypes).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired(false);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.RoleId).IsRequired(true);
            builder.HasOne(x => x.Chemist).WithOne(x => x.user).HasForeignKey<Chemist>(x => x.ChemistId);
            builder.HasOne(x => x.Client).WithMany().HasForeignKey(x => x.ClientId);
            builder.HasMany(x => x.UserDevices).WithOne().HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.GeoZones).WithOne().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.UserAdditionalPermissions).WithOne().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.UserExcludedRolePermissions).WithOne().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
