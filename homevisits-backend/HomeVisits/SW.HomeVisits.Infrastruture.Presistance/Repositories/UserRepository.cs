using System;
using System.Threading.Tasks;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using SW.HomeVisits.Infrastruture.Data;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace SW.HomeVisits.Infrastruture.Presistance.Repositories
{
    internal class UserRepository : EfRepository<HomeVisitsDomainContext>, IUserRepository
    {
        public UserRepository(HomeVisitsDomainContext context) : base(context)
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

        public void DeleteUser(Guid userId)
        {
            var user = Context.Users.Find(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.DeleteUser();
            Context.Users.Update(user);
        }

        public async Task<User> FindUserByNormalizedEmail(string email)
        {
            return await Task.FromResult(Context.Users.SingleOrDefault(x => x.NormalizedEmail == email));
        }

        public async Task<User> FindUserByUserName(string userName)
        {
            return await Task.FromResult(Context.Users.SingleOrDefault(x => x.NormalizedUserName == userName));
        }

        public User FindUserId(Guid userId)
        {
            var user = Context.Users.Find(userId);
            if (user != null)
            {
                Context.Entry(user).Reference(x => x.Chemist).Load();
                if (user.Chemist != null)
                    Context.Entry(user.Chemist).Collection(x => x.ChemistsGeoZones).Load();
                Context.Entry(user).Collection(x => x.GeoZones).Load();
                Context.Entry(user).Collection(x => x.UserAdditionalPermissions).Load();
                Context.Entry(user).Collection(x => x.UserExcludedRolePermissions).Load();
            }

            return user;
        }

        public void PresistNewUser(User user)
        {
            Context.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            Context.Users.Update(user);
        }

        public void AddUserDevice(UserDevice userDevice)
        {
            Context.UserDevices.Add(userDevice);
        }

        public int GetLatestChemistCode(Guid clientId)
        {
            var codes = Context.Chemists.Where(x => x.user.ClientId == clientId);
            return !codes.Any() ? 0 : codes.Max(x => x.Code);
        }

        public int GetLatestClientUserCode(Guid clientId)
        {
            var codes = Context.Users.Where(x => x.ClientId == clientId);
            return !codes.Any() ? 0 : codes.Max(x => x.Code);
        }

        public void PresistNewChmistSchedule(ChemistSchedule schedule)
        {
            Context.ChemistSchedules.Add(schedule);
        }

        public void PresistNewChmistPermit(ChemistPermit permit)
        {
            Context.ChemistPermits.Add(permit);
        }

        public void UpdateChmistPermit(ChemistPermit permit)
        {
            Context.ChemistPermits.Update(permit);
        }

        public void DeleteChmistPermit(Guid permitId)
        {
            var permit = Context.ChemistPermits.Find(permitId);
            if (permit == null)
            {
                throw new Exception("Permit not found");
            }
            permit.IsDeleted = true;
            Context.ChemistPermits.Update(permit);
        }

        public ChemistPermit GetChemistPermitById(Guid permitId)
        {
            var permit = Context.ChemistPermits.Find(permitId);
            if (permit == null)
            {
                throw new Exception("Permit not found");
            }
            return permit;
        }

        public ChemistSchedule GetChemistSchedule(Guid chemistScheduleId)
        {
            var schedule = Context.ChemistSchedules.Find(chemistScheduleId);
            if (schedule != null)
                Context.Entry(schedule).Collection(x => x.ScheduleDays).Load();
            return schedule;
        }

        public void ChangeEntityStateToDeleted<T>(T entity)
        {
            Context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void UpdateChemistSchedule(ChemistSchedule chemistSchedule)
        {
            Context.Update(chemistSchedule);
        }

        public void AddUserAdditionalPermission(UserAdditionalPermission userAdditionalPermissions)
        {
            Context.UserAdditionalPermissions.Add(userAdditionalPermissions);
        }

        public void AddUserExcludedRolePermission(UserExcludedRolePermission userExcludedRolePermissions)
        {
            Context.UserExcludedRolePermissions.Add(userExcludedRolePermissions);
        }

        public void AddUserAdditionalPermission(List<UserAdditionalPermission> userAdditionalPermissions)
        {
            Context.UserAdditionalPermissions.AddRange(userAdditionalPermissions);
        }

        public void AddUserExcludedRolePermission(List<UserExcludedRolePermission> userExcludedRolePermissions)
        {
            Context.UserExcludedRolePermissions.AddRange(userExcludedRolePermissions);
        }

        public void UpdateUserAdditionalPermission(UserAdditionalPermission userAdditionalPermissions)
        {
            Context.UserAdditionalPermissions.Update(userAdditionalPermissions);
        }

        public void UpdateUserExcludedRolePermission(UserExcludedRolePermission userExcludedRolePermissions)
        {
            Context.UserExcludedRolePermissions.Update(userExcludedRolePermissions);
        }

        public void UpdateUserAdditionalPermission(List<UserAdditionalPermission> userAdditionalPermissions)
        {
            Context.UserAdditionalPermissions.UpdateRange(userAdditionalPermissions);
        }

        public void UpdateUserExcludedRolePermission(List<UserExcludedRolePermission> userExcludedRolePermissions)
        {
            Context.UserExcludedRolePermissions.UpdateRange(userExcludedRolePermissions);
        }

        public IQueryable<UserAdditionalPermission> GetUserAdditionalPermission(Guid userId)
        {
            return Context.UserAdditionalPermissions.Where(p => p.UserId == userId && !p.IsDeleted).AsNoTracking();
        }

        public IQueryable<UserExcludedRolePermission> GetUserExcludedRolePermission(Guid userId)
        {
            return Context.UserExcludedRolePermissions.Where(p => p.UserId == userId && !p.IsDeleted).AsNoTracking();
        }
    }
}
