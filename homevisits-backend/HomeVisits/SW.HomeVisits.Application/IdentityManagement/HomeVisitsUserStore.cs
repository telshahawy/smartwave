using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.Identity;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.IdentityManagement
{
    public class HomeVisitsUserStore : IUserStore<User>,IUserPasswordStore<User>,IUserEmailStore<User>,IUserSecurityStampStore<User>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        public IdentityErrorDescriber _errorDescriber { get; }
        public HomeVisitsUserStore(IHomeVisitsUnitOfWork unitOfWork, ILog log, IdentityErrorDescriber errorDescriber = null)
        {
            _unitOfWork = unitOfWork;
            _log = log;
            _errorDescriber = errorDescriber;
        }
        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            cancellationToken.ThrowIfCancellationRequested();
            var repository = _unitOfWork.Repository<IUserRepository>();
            repository.PresistNewUser(user);    
            _unitOfWork.SaveChanges();
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var repository = _unitOfWork.Repository<IUserRepository>();
            repository.DeleteUser(user.Id);

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

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //ThrowIfDisposed();
            var repository = _unitOfWork.Repository<IUserRepository>();
            //cancellationToken.ThrowIfCancellationRequested();
            var user = repository.FindUserId(Guid.Parse(userId));
            //if (user != null && user.IsActive == true && user.IsDeleted != true)
            //{
                return await Task.FromResult(user);
            //}
            //return null;

        }
        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //ThrowIfDisposed();
            var repository = _unitOfWork.Repository<IUserRepository>();
            var user =await repository.FindUserByUserName(normalizedUserName);
            //cancellationToken.ThrowIfCancellationRequested();

            if (user != null && user.IsActive == true && user.IsDeleted != true)
            {
                return await Task.FromResult(user);
            }
            return null;
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserId.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {

            cancellationToken.ThrowIfCancellationRequested();
            //ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (normalizedName == null)
            {
                throw new ArgumentNullException(nameof(normalizedName));
            }

            user.SetNormaliedUserName(normalizedName);

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            user.SetUserName(userName);

            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            cancellationToken.ThrowIfCancellationRequested();
            var repository = _unitOfWork.Repository<IUserRepository>();
            repository.UpdateUser(user);
            _unitOfWork.SaveChanges();
            return Task.FromResult(IdentityResult.Success);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.SetPasswordHash(passwordHash);
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.SetEmail(email);
            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.SetEmailConfirmed(confirmed);
            return Task.CompletedTask;
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //ThrowIfDisposed();
            var repository = _unitOfWork.Repository<IUserRepository>();
            //cancellationToken.ThrowIfCancellationRequested();
            return await repository.FindUserByNormalizedEmail(normalizedEmail);
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.SetNormalizedEmail(normalizedEmail);
            return Task.CompletedTask;
        }

        public Task SetSecurityStampAsync(User user, string stamp, CancellationToken cancellationToken)
        {
            user.SetSecurityStamp(stamp);
            return Task.CompletedTask;
        }

        public Task<string> GetSecurityStampAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SecurityStamp);
        }
    }
}
    