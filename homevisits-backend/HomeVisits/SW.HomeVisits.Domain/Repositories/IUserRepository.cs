using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Domain.Repositories
{
    public interface IUserRepository : IDisposableRepository
    {
        void PresistNewUser(User user);
        void DeleteUser(Guid userId);
        User FindUserId(Guid userId);
        Task<User> FindUserByUserName(string userName);
        Task<User> FindUserByNormalizedEmail(string email);
        void UpdateUser(User user);
        void AddUserDevice(UserDevice userDevice);
        int GetLatestChemistCode(Guid clientId);
        int GetLatestClientUserCode(Guid clientId);
        void PresistNewChmistSchedule(ChemistSchedule schedule);
        void PresistNewChmistPermit(ChemistPermit permit);
        void UpdateChmistPermit(ChemistPermit permit);
        void DeleteChmistPermit(Guid permitId);
        ChemistPermit GetChemistPermitById(Guid permitId);
        void ChangeEntityStateToAdded<T>(T entity);
        void ChangeEntityStateToModified<T>(T entity);
        void ChangeEntityStateToDeleted<T>(T entity);
        ChemistSchedule GetChemistSchedule(Guid chemistScheduleId);
        void UpdateChemistSchedule(ChemistSchedule chemistSchedule);
        IQueryable<UserAdditionalPermission> GetUserAdditionalPermission(Guid userId);
        IQueryable<UserExcludedRolePermission> GetUserExcludedRolePermission(Guid userId);
        void AddUserAdditionalPermission(UserAdditionalPermission userAdditionalPermissions);
        void AddUserExcludedRolePermission(UserExcludedRolePermission userExcludedRolePermissions);
        void AddUserAdditionalPermission(List<UserAdditionalPermission> userAdditionalPermissions);
        void AddUserExcludedRolePermission(List<UserExcludedRolePermission> userExcludedRolePermissions);
        void UpdateUserAdditionalPermission(UserAdditionalPermission userAdditionalPermissions);
        void UpdateUserExcludedRolePermission(UserExcludedRolePermission userExcludedRolePermissions);
        void UpdateUserAdditionalPermission(List<UserAdditionalPermission> userAdditionalPermissions);
        void UpdateUserExcludedRolePermission(List<UserExcludedRolePermission> userExcludedRolePermissions);
    }
}
