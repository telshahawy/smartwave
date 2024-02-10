using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using SW.HomeVisits.Infrastruture.Data;

namespace SW.HomeVisits.Infrastruture.Presistance.Repositories
{
    internal class SystemPagePermissionRepository : EfRepository<HomeVisitsDomainContext>, ISystemPagePermissionRepository
    {
        public SystemPagePermissionRepository(HomeVisitsDomainContext context) : base(context)
        {
        }

        public IQueryable<SystemPagePermission> GetSystemPagePermissions()
        {
            return Context.SystemPagePermissions.AsNoTracking();
        }

        public SystemPagePermission GetSystemPagePermissionById(int SystemPagePermissionId)
        {
            return Context.SystemPagePermissions.AsNoTracking().SingleOrDefault(r => r.SystemPagePermissionId == SystemPagePermissionId);
        }

        public void AddSystemPagePermission(SystemPagePermission SystemPagePermission)
        {
            Context.SystemPagePermissions.Add(SystemPagePermission);
        }

        public void UpdateSystemPagePermission(SystemPagePermission SystemPagePermission)
        {
            Context.SystemPagePermissions.Update(SystemPagePermission);
        }
    }
}
