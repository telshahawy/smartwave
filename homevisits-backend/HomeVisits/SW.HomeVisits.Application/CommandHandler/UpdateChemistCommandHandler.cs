using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.Identity;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class UpdateChemistCommandHandler : ICommandHandler<IUpdateChemistCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly UserManager<User> _userManager;
        //private readonly ILog _log;

        public UpdateChemistCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _log = log;
            _userManager = userManager;
        }
        public void Handle(IUpdateChemistCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IUserRepository>();
                var user = _userManager.FindByIdAsync(command.UserId.ToString()).GetAwaiter().GetResult();
                if (user == null)
                    throw new Exception("user not Found");

                user.UpdateChemist(command.Name, command.Gender, command.PhoneNumber, command.BirthDate, command.PersonalPhoto,
                    command.ExpertChemist, command.IsActive, command.JoinDate);

                foreach (var id in command.GeoZoneIds)
                {

                    if (!user.Chemist.ChemistsGeoZones.Any(x => x.GeoZoneId == id))
                    {
                        var geoZone = new ChemistAssignedGeoZone
                        {
                            ChemistAssignedGeoZoneId = Guid.NewGuid(),
                            GeoZoneId = id,
                            ChemistId = command.UserId,
                            CreatedAt = DateTime.Now,
                            CreatedBy = user.CreatedBy.GetValueOrDefault(),
                            IsActive = true,
                            IsDeleted = false
                        };
                        user.Chemist.ChemistsGeoZones.Add(geoZone);
                        repository.ChangeEntityStateToAdded(geoZone);
                    }
                    else
                    {
                        var geoZone = user.Chemist.ChemistsGeoZones.SingleOrDefault(x => x.GeoZoneId == id);
                        geoZone.IsDeleted = false;
                        repository.ChangeEntityStateToModified(geoZone);
                    }
                }
                var geoZonesToBeDeleted = user.Chemist.ChemistsGeoZones.Where(x => !command.GeoZoneIds.Contains(x.GeoZoneId));
                foreach (var geoZone in geoZonesToBeDeleted)
                {
                    geoZone.IsDeleted = true;
                    repository.ChangeEntityStateToModified(geoZone);
                }
                var res = _userManager.UpdateAsync(user).GetAwaiter().GetResult();
                if (!res.Succeeded)
                {
                    throw new Exception(res.Errors.First().Code);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
