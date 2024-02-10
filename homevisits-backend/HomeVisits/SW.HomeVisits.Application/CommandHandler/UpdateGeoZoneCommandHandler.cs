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
    public class UpdateGeoZoneCommandHandler : ICommandHandler<IUpdateGeoZoneCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        //private readonly ILog _log;

        public UpdateGeoZoneCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(IUpdateGeoZoneCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IGeoZonesRepository>();
                var geoZoneFromDB = repository.GetGeoZone(command.GeoZoneId);
                if(geoZoneFromDB == null)
                {
                    throw new Exception("Area not found");
                }
                geoZoneFromDB.GeoZoneId = command.GeoZoneId;
                geoZoneFromDB.Code = geoZoneFromDB.Code;
                geoZoneFromDB.NameAr = command.Name == null? geoZoneFromDB.NameAr:command.Name;
                geoZoneFromDB.NameEn = command.Name == null ? geoZoneFromDB.NameEn : command.Name;
                geoZoneFromDB.KmlFilePath = command.KmlFilePath;
                geoZoneFromDB.KmlFileName = command.KmlFileName;
                geoZoneFromDB.MappingCode = command.MappingCode;
                geoZoneFromDB.GovernateId = command.GovernateId;
                geoZoneFromDB.IsActive = command.IsActive;

                foreach (var item in command.TimeZoneFrames)
                {

                    if (!geoZoneFromDB.TimeZones.Any(x => x.TimeZoneFrameId == item.TimeZoneFrameId))
                    {
                        var timeZone = new TimeZoneFrame
                        {
                            TimeZoneFrameId = Guid.NewGuid(),
                            NameAr = item.NameAr,
                            NameEN = item.NameEn,
                            VisitsNoQouta = item.VisitsNoQuota,
                            StartTime = TimeSpan.Parse(item.StartTime),
                            EndTime = TimeSpan.Parse(item.EndTime),
                            BranchDispatch = item.BranchDispatch,
                            GeoZoneId = geoZoneFromDB.GeoZoneId
                        };
                        item.TimeZoneFrameId = timeZone.TimeZoneFrameId;
                        geoZoneFromDB.TimeZones.Add(timeZone);
                        repository.ChangeEntityStateToAdded(timeZone);
                    }
                    else
                    {
                        var timeZone = geoZoneFromDB.TimeZones.SingleOrDefault(x => x.TimeZoneFrameId == item.TimeZoneFrameId);
                        timeZone.IsDeleted = false;
                        timeZone.NameAr = item.NameAr;
                        timeZone.NameEN = item.NameEn;
                        timeZone.VisitsNoQouta = item.VisitsNoQuota;
                        timeZone.StartTime = TimeSpan.Parse(item.StartTime);
                        timeZone.EndTime = TimeSpan.Parse(item.EndTime);
                        timeZone.BranchDispatch = item.BranchDispatch;

                        repository.ChangeEntityStateToModified(timeZone);
                    }
                }
                var timeZonesToBeDeleted = geoZoneFromDB.TimeZones.Where(x => !command.TimeZoneFrames.Where(t=>t.TimeZoneFrameId != Guid.Empty).Select(t=>t.TimeZoneFrameId).Contains(x.TimeZoneFrameId));
                foreach (var timeZone in timeZonesToBeDeleted)
                {
                    timeZone.IsDeleted = true;
                    repository.ChangeEntityStateToModified(timeZone);
                }

                repository.UpdateGeoZone(geoZoneFromDB);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
