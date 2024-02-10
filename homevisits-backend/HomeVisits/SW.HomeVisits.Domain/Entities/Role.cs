using System;
using System.Collections.Generic;
using System.Linq;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class Role : Entity<Guid>
    {
        public static Role CreateNewRole(
            Guid roleId, Guid? clientId,
            int code,
            string nameAr,
            string nameEn,
            string description,
            bool isActive,
            Guid createdBy,
            int defaultPageId)
        {
            var role = new Role
            {
                ClientId = clientId,
                Code = code,
                CreatedAt = DateTime.Now,
                CreatedBy = createdBy,
                Description = description,
                IsActive = isActive,
                NameAr = nameAr,
                NameEn = nameEn,
                RoleId = roleId,
                DefaultPageId = defaultPageId
            };
            return role;
        }

        public void SetRoleCode(int code)
        {
            Code = code;
        }

        public void AddPermission(int systemPagePermissionId)
        {
            if (RolePermissions == null)
                RolePermissions = new List<RolePermission>();

            if (RolePermissions.Any(x => x.SystemPagePermissionId == systemPagePermissionId))
                throw new Exception("Permission already exists");

            RolePermissions.Add(new RolePermission
            {
                RoleId = RoleId,
                SystemPagePermissionId = systemPagePermissionId,
                RolePermissionId = Guid.NewGuid()
            });
        }

        public void AddGeoZone(Guid geoZoneId)
        {
            if (GeoZones == null)
                GeoZones = new List<RoleGeoZone>();

            if (GeoZones.Any(x => x.GeoZoneId == geoZoneId))
                throw new Exception("GeoZone already exists");

            GeoZones.Add(new RoleGeoZone
            {
                RoleId = RoleId,
                GeoZoneId = geoZoneId,
                RoleGeoZoneId = Guid.NewGuid()
            });
        }

        public void RemovePermission(int systemPagePermissionId)
        {
            var rolePermission = RolePermissions.SingleOrDefault(x => x.SystemPagePermissionId == systemPagePermissionId);
            if (rolePermission == null)
                throw new Exception("Permission not found");

            RolePermissions.Remove(rolePermission);
        }

        public void RemoveGeoZone(Guid geoZoneId)
        {
            var geoZone = GeoZones.SingleOrDefault(x => x.GeoZoneId == geoZoneId);
            if (geoZone == null)
                throw new Exception("GeoZone not found");

            GeoZones.Remove(geoZone);
        }

        public void UpdateRole(
            string nameAr,
            string nameEn,
            string description,
            bool isActive,
            int defaultPageId)
        {
            NameAr = nameAr;
            NameEn = nameEn;
            Description = description;
            IsActive = isActive;
            DefaultPageId = defaultPageId;
        }

        public Guid RoleId { get => Id; private set => Id = value; }
        public Guid? ClientId { get; private set; }
        public int Code { get; private set; }
        public string NameAr { get; private set; }
        public string NameEn { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public Guid CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted { get; set; }
        public int DefaultPageId { get; set; }
        public bool IsStaticRole { get; set; }
        public Client Client { get; set; }
        public SystemPage DefaultPage { get; set; }
        public ICollection<RolePermission> RolePermissions { get; private set; }
        public ICollection<RoleGeoZone> GeoZones { get; private set; }
    }
}
