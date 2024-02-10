using Common.Logging;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class AddGeoZoneCommandHandler : ICommandHandler<IAddGeoZoneCommand>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;

        public AddGeoZoneCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(IAddGeoZoneCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IGeoZonesRepository>();
                int geoZoneLatestNo = repository.GetLatestGeoZoneCode() + 1;

                var geoZone = new GeoZone
                {
                    GeoZoneId = command.GeoZoneId,
                    NameAr = command.NameAr,
                    NameEn = command.NameEn,
                    KmlFilePath = command.KmlFilePath,
                    KmlFileName = command.KmlFileName,
                    Code = geoZoneLatestNo,
                    MappingCode = command.MappingCode,
                    GovernateId = command.GovernateId,
                    IsActive = command.IsActive,
                    CreatedAt = DateTime.Now,
                    CreatedBy = command.CreatedBy,
                    TimeZones = command.TimeZoneFrames.Select(item => new TimeZoneFrame
                    {
                        TimeZoneFrameId = item.TimeZoneFrameId,
                        NameAr = command.NameAr,
                        NameEN = command.NameAr,
                        VisitsNoQouta = item.VisitsNoQuota,
                        StartTime = TimeSpan.Parse(item.StartTime),
                        EndTime = TimeSpan.Parse(item.EndTime),
                        BranchDispatch = item.BranchDispatch,
                        GeoZoneId = command.GeoZoneId

                    }).ToList()

                };

                repository.PresistNewGeoZone(geoZone);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
