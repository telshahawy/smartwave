using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;
namespace SW.HomeVisits.Domain.Repositories
{
    public interface ISystemPagePermissionRepository : IDisposableRepository
    {
        IQueryable<SystemPagePermission> GetSystemPagePermissions();
        void AddSystemPagePermission(SystemPagePermission systemPagePermission);
        void UpdateSystemPagePermission(SystemPagePermission systemPagePermission);
        SystemPagePermission GetSystemPagePermissionById(int systemPagePermissionId);
    }
}
