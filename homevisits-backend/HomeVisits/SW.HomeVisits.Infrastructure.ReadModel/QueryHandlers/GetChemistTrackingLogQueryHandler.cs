using System;
using Common.Logging;
using SW.Framework.Cqrs;
using System.Linq;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using SW.HomeVisits.Application.Abstract.Dtos;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using SW.HomeVisits.Domain.Enums;
using System.Globalization;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    internal class GetChemistTrackingLogQueryHandler : IQueryHandler<IGetChemistTrackingLogQuery, IGetChemistTrackingLogQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetChemistTrackingLogQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetChemistTrackingLogQueryResponse Read(IGetChemistTrackingLogQuery query)
        {
            IQueryable<ChemistTrackingLogView> dbQuery = _context.ChemistTrackingLogViews;

            if (query != null)
            {
                dbQuery = dbQuery.Where(a => a.ChemistId == query.ChemistId).OrderByDescending(a => a.CreationDate);
            }

            return new GetChemistTrackingLogQueryResponse()
            {
                ChemistTrackingLog = dbQuery.Select(a => new ChemistTrackingLogDto
                {
                    ChemistTrackingLogId = a.ChemistTrackingLogId,
                    ChemistId = a.ChemistId,
                    CreationDate = a.CreationDate,
                    Longitude = a.Longitude,
                    Latitude = a.Latitude,
                    DeviceSerialNumber = a.DeviceSerialNumber,
                    MobileBatteryPercentage = a.MobileBatteryPercentage,
                    UserName = a.UserName
                }).FirstOrDefault()
            } as IGetChemistTrackingLogQueryResponse;
        }
    }
}
