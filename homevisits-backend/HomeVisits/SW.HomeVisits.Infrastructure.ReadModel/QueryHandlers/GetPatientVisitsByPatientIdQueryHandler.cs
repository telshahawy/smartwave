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
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    internal class GetPatientVisitsByPatientIdQueryHandler : IQueryHandler<IGetPatientVisitsByPatientIdQuery, IGetPatientVisitsByPatientIdQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetPatientVisitsByPatientIdQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetPatientVisitsByPatientIdQueryResponse Read(IGetPatientVisitsByPatientIdQuery query)
        {
            IQueryable<PatientVisitsView> dbQuery = _context.PatientVisitsViews;
            if (query != null)
            {
                dbQuery = dbQuery.Where(p => p.PatientId == query.PatientId && p.VisitStatusTypeId != (int)VisitStatusTypes.Done);
            }

            return new GetPatientVisitsByPatientIdQueryResponse()
            {
                PatientVistis = dbQuery.Select(p => new PatientVistisDto
                {
                    VisitId = p.VisitId,
                    VisitCode=p.VisitCode,
                    VisitNo = p.VisitNo,
                    VisitDate = p.VisitDate,
                    GeoZoneId = p.GeoZoneId,
                    ZoneName = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? p.ZoneNameAr : p.ZoneNameEn,
                    VisitStatusTypeId = p.VisitStatusTypeId,
                    StatusName = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? p.StatusNameAr : p.StatusNameEn,
                    PatientId = p.PatientId,
                    VisitDateString = p.VisitDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    VisitTime = p.VisitDate.ToString("hh:mm tt", query.CultureName == Application.Abstract.Enum.CultureNames.ar ? new CultureInfo("ar-EG") : new CultureInfo("en-US"))
                }).ToList()
            } as IGetPatientVisitsByPatientIdQueryResponse;
        }
    }
}
