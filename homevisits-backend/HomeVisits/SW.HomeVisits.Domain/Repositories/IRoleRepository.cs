using System;
using System.Linq;
using System.Threading.Tasks;
using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Domain.Repositories
{
    public interface IRoleRepository : IDisposableRepository
    {
        void PresistNewRole(Role role);
        void DeleteRole(Guid roleId);
        Task<Role> FindRoleId(Guid roleId);
        Task<Role> FindRoleByName(string name);
        Task<Role> FindRoleByCode(int code);
        int GetLatestRoleCode(Guid clientId);
        bool RoleNameExists(string name, Guid clientId, Guid roleId);
        void UpdateRole(Role role);
        void ChangeEntityStateToModified<T>(T entity);
        void ChangeEntityStateToAdded<T>(T entity);
        IQueryable<RolePermission> GetRoleSystemPagePermissions(Guid roleId);
    }
}
