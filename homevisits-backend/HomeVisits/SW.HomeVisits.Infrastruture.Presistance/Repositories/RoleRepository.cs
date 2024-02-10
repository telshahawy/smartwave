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
    internal class RoleRepository : EfRepository<HomeVisitsDomainContext>, IRoleRepository
    {
        public RoleRepository(HomeVisitsDomainContext context) : base(context)
        {
        }

        public void ChangeEntityStateToAdded<T>(T entity)
        {
            Context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }

        public void ChangeEntityStateToModified<T>(T entity)
        {
            Context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void DeleteRole(Guid roleId)
        {
            var role = Context.Roles.Find(roleId);
            if (role == null)
            {
                throw new Exception("User not found");
            }
            role.IsDeleted = true;
            Context.Roles.Update(role);
        }

        public async Task<Role> FindRoleByName(string name)
        {
            return await Task.FromResult(Context.Roles.SingleOrDefault(x => x.NameEn.Trim().ToLower() == name.Trim().ToLower() || x.NameAr.Trim().ToLower() == name.Trim().ToLower()));
        }

        public async Task<Role> FindRoleByCode(int code)
        {
            return await Task.FromResult(Context.Roles.SingleOrDefault(x => x.Code == code));
        }

        public async Task<Role> FindRoleId(Guid roleId)
        {
            var role = Context.Roles.Find(roleId);
            Context.Entry(role).Collection(x => x.RolePermissions).Load();
            Context.Entry(role).Collection(x => x.GeoZones).Load();
            return await Task.FromResult(role);
        }

        public int GetLatestRoleCode(Guid clientId)
        {
            var codes = Context.Roles.Where(x => x.ClientId == clientId);
            return !codes.Any() ? 0 : codes.Max(x => x.Code);
        }

        public void PresistNewRole(Role role)
        {
            Context.Roles.Add(role);
        }

        public bool RoleNameExists(string name, Guid clientId, Guid roleId)
        {
            var result = Context.Roles.Any(x => x.ClientId == clientId && x.NameAr == name && x.RoleId != roleId);
            return result;
        }

        public void UpdateRole(Role role)
        {
            //Context.Entry(role.GeoZones).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.Roles.Update(role);
        }

        public IQueryable<RolePermission> GetRoleSystemPagePermissions( Guid roleId)
        {
            return Context.RolePermissions.Where(p => p.RoleId == roleId && !p.IsDeleted).AsNoTracking();
        }
    }
}
