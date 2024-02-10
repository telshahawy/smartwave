using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.Identity;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.IdentityManagement
{
    public class HomeVisitsRoleStore : IRoleStore<Role>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        public IdentityErrorDescriber _errorDescriber { get; }
        public HomeVisitsRoleStore(IHomeVisitsUnitOfWork unitOfWork, ILog log, IdentityErrorDescriber errorDescriber = null)
        {
            _unitOfWork = unitOfWork;
            _log = log;
            _errorDescriber = errorDescriber;
        }
        public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            cancellationToken.ThrowIfCancellationRequested();
            var repository = _unitOfWork.Repository<IRoleRepository>();
            repository.PresistNewRole(role);
            _unitOfWork.SaveChanges();
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //ThrowIfDisposed();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            var repository = _unitOfWork.Repository<IRoleRepository>();
            repository.DeleteRole(role.RoleId);

            try
            {
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                return Task.FromResult(IdentityResult.Failed(_errorDescriber.ConcurrencyFailure()));
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //ThrowIfDisposed();
            var repository = _unitOfWork.Repository<IRoleRepository>();
            //cancellationToken.ThrowIfCancellationRequested();
            return await repository.FindRoleId(Guid.Parse(roleId));
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //ThrowIfDisposed();
            var repository = _unitOfWork.Repository<IRoleRepository>();
            //cancellationToken.ThrowIfCancellationRequested();
            return await repository.FindRoleByName(normalizedRoleName);
        }
        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Code.ToString().Normalize());
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.RoleId.ToString());
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Code.ToString().Normalize());
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
          return  Task.CompletedTask;
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            role.SetRoleCode(int.Parse(roleName));
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            cancellationToken.ThrowIfCancellationRequested();
            var repository = _unitOfWork.Repository<IRoleRepository>();
            repository.UpdateRole(role);
            _unitOfWork.SaveChanges();
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
