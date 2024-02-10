using System;
using System.Collections.Generic;
using System.Linq;
using SW.Framework.Domain;
using SW.HomeVisits.Domain.Enums;

namespace SW.HomeVisits.Domain.Entities
{
    public class User : Entity<Guid>
    {
        public User()
        {

        }
        public static User CreatePatientLogin(
            Guid userId,
            string userName,
            string name,
            Guid clientId,
            bool isActive,
            Guid createdBy,
            Guid RoleId,
            int code,
            string phoneNo
            )
        {
            var user = new User
            {
                UserId = userId,
                UserType = (int)UserTypes.Patient,
                Name = name,
                IsActive = isActive,
                ClientId = clientId,
                UserName = userName,
                CreatedBy = createdBy,
                CreatedAt = DateTime.Now,
                RoleId = RoleId,
                Code = code,
                PhoneNumber = phoneNo
            };
            return user;
        }
        public static User CreateChemist(
            Guid userId,
            string userName,
            string name,
            int code,
            int gender,
            string phoneNumber,
            DateTime birthDate,
            string personalPhoto,
            bool expertChemist,
            bool isActive,
            Guid clientId,
            DateTime joinDate,
            int dob,
            Guid createdBy,
            List<Guid> GeoZoneIds,
            Guid roleId
            )
        {
            var user = new User
            {
                UserId = userId,
                Gender = gender,
                Name = name,
                PhoneNumber = phoneNumber,
                BirthDate = birthDate,
                PersonalPhoto = personalPhoto,
                IsActive = isActive,
                ClientId = clientId,
                UserName = userName,
                UserType = (int)UserTypes.Chemist,
                RoleId = roleId,
                Chemist = new Chemist
                {
                    ChemistId = userId,
                    Code = code,
                    DOB = dob,
                    ExpertChemist = expertChemist,
                    JoinDate = joinDate,
                }
            };
            user.Chemist.ChemistsGeoZones = new List<ChemistAssignedGeoZone>();
            foreach (var item in GeoZoneIds)
            {
                user.Chemist.ChemistsGeoZones.Add(new ChemistAssignedGeoZone
                {
                    ChemistAssignedGeoZoneId = Guid.NewGuid(),
                    ChemistId = user.UserId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = createdBy,
                    GeoZoneId = item,
                    IsActive = true,
                    IsDeleted = false,

                });
            }
            return user;
        }
        public static User CreateClientUser(Guid userId,
            string userName,
            string name,
            int code,
            string phoneNumber,
            bool isActive,
            Guid clientId,
            Guid createdBy,
            List<Guid> GeoZoneIds,
            Guid RoleId
            )
        {
            var user = new User
            {
                UserId = userId,
                Name = name,
                PhoneNumber = phoneNumber,
                IsActive = isActive,
                ClientId = clientId,
                UserName = userName,
                UserType = (int)UserTypes.ClientAdmin,
                CreatedBy = createdBy,
                CreatedAt = DateTime.Now,
                RoleId = RoleId,
                Code = code
            };

            user.GeoZones = new List<UserGeoZone>();
            foreach (var item in GeoZoneIds)
            {
                user.GeoZones.Add(new UserGeoZone
                {
                    UserGeoZoneId = Guid.NewGuid(),
                    UserId = user.UserId,
                    GeoZoneId = item,
                    IsActive = true,
                    IsDeleted = false,
                });
            }
            return user;
        }

        public void UpdateClientUser(
            string name,
            string phoneNumber,
            bool isActive,
            Guid roleId)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            IsActive = isActive;
            RoleId = roleId;
        }

        public void UpdateChemist(

            string name,
            int gender,
            string phoneNumber,
            DateTime birthDate,
            string personalPhoto,
            bool expertChemist,
            bool isActive,
            DateTime joinDate
        )
        {
            Gender = gender;
            Name = name;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            PersonalPhoto = personalPhoto;
            IsActive = isActive;
            UserType = (int)UserTypes.Chemist;
            Chemist.ExpertChemist = expertChemist;
            Chemist.JoinDate = joinDate;

        }

        public void AddGeoZone(Guid geoZoneId)
        {
            var geoZone = GeoZones.SingleOrDefault(x => x.GeoZoneId == geoZoneId);

            if (geoZone != null)
            {
                geoZone.IsActive = true;
                geoZone.IsDeleted = false;
            }
            else
            {
                GeoZones.Add(new UserGeoZone
                {
                    GeoZoneId = geoZoneId,
                    UserId = UserId,
                    IsActive = true,
                    IsDeleted = false,
                    UserGeoZoneId = Guid.NewGuid()
                });
            }

        }

        public void RemoveGeoZone(Guid geoZoneId)
        {
            var geozone = GeoZones.SingleOrDefault(x => x.GeoZoneId == geoZoneId);
            if (geozone != null)
            {
                geozone.IsDeleted = true;
            }
        }

        public void DeleteUser()
        {
            IsDeleted = true;
        }

        public void UnDeleteUser()
        {
            IsDeleted = false;
        }

        public void SetNormaliedUserName(string normalizedName)
        {
            NormalizedUserName = normalizedName;
        }

        public void SetUserName(string userName)
        {
            UserName = userName;
        }

        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetEmailConfirmed(bool emailConfirmed)
        {
            EmailConfirmed = emailConfirmed;
        }

        public void SetNormalizedEmail(string normalizedEmail)
        {
            NormalizedEmail = normalizedEmail;
        }

        public void SetSecurityStamp(string securityStamp)
        {
            SecurityStamp = securityStamp;
        }

        public UserAdditionalPermission AddUserAdditionalPermission(int systemPagePermissionId)
        {
            UserAdditionalPermission userAdditionalPermission = null;
            if (UserAdditionalPermissions == null)
                UserAdditionalPermissions = new List<UserAdditionalPermission>();

            if (!UserAdditionalPermissions.Any(x => x.SystemPagePermissionId == systemPagePermissionId && !x.IsDeleted))
            {
                userAdditionalPermission = new UserAdditionalPermission
                {
                    UserId = UserId,
                    IsDeleted = false,
                    SystemPagePermissionId = systemPagePermissionId,
                    UserAdditionalPermissionId = Guid.NewGuid()
                };
                UserAdditionalPermissions.Add(userAdditionalPermission);
            }
            return userAdditionalPermission;
        }

        public UserExcludedRolePermission AddUserExcludedRolePermissions(int systemPagePermissionId, Guid roleId)
        {
            UserExcludedRolePermission userExcludedRolePermission = null;
            if (UserExcludedRolePermissions == null)
                UserExcludedRolePermissions = new List<UserExcludedRolePermission>();

            if (!UserExcludedRolePermissions.Any(x => x.SystemPagePermissionId == systemPagePermissionId && !x.IsDeleted))
            {
                userExcludedRolePermission = new UserExcludedRolePermission
                {
                    UserId = UserId,
                    RoleId = roleId,
                    IsDeleted = false,
                    SystemPagePermissionId = systemPagePermissionId,
                    UserExcludedRolePermissionId = Guid.NewGuid()
                };
                UserExcludedRolePermissions.Add(userExcludedRolePermission);
            }
            return userExcludedRolePermission;
        }

        public Guid UserId { get => Id; set => Id = value; }
        public int Code { get; set; }
        public Guid RoleId { get; set; }
        public int UserType { get; set; }
        public Guid? ClientId { get; set; }
        public string Name { get; private set; }
        public string UserName { get; private set; }
        public int? Gender { get; set; }
        public virtual string NormalizedUserName { get; private set; }
        public virtual string Email { get; private set; }
        public virtual string NormalizedEmail { get; private set; }
        public virtual bool EmailConfirmed { get; private set; }
        public virtual string PasswordHash { get; private set; }
        public virtual string SecurityStamp { get; private set; }
        public virtual string ConcurrencyStamp { get; private set; } = Guid.NewGuid().ToString();
        public virtual string PhoneNumber { get; private set; }
        public virtual bool PhoneNumberConfirmed { get; private set; }
        public virtual bool TwoFactorEnabled { get; private set; }
        public virtual DateTimeOffset? LockoutEnd { get; private set; }
        public virtual bool LockoutEnabled { get; private set; }
        public virtual int AccessFailedCount { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public string PersonalPhoto { get; private set; }
        public int UserCreationTypes { get; private set; }
        public Guid? CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }
        public Chemist Chemist { get; private set; }
        public Client Client { get; set; }
        public Role Role { get; set; }
        public ICollection<UserDevice> UserDevices { get; set; }
        public ICollection<UserGeoZone> GeoZones { get; set; }
        public ICollection<UserAdditionalPermission> UserAdditionalPermissions { get; set; }
        public ICollection<UserExcludedRolePermission> UserExcludedRolePermissions { get; set; }
    }
}
