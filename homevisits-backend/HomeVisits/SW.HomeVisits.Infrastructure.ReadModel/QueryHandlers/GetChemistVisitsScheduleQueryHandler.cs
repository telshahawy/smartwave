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
using System.Data.Entity.Core.Common.CommandTrees;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetChemistVisitsScheduleQueryHandler : IQueryHandler<IGetChemistVisitsScheduleQuery, IGetChemistVisitsScheduleQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetChemistVisitsScheduleQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetChemistVisitsScheduleQueryResponse Read(IGetChemistVisitsScheduleQuery query)
        {
            IQueryable<ChemistVisitsScheduleView> dbQuery = _context.ChemistVisitsScheduleView;

            if (query != null)
            {
                dbQuery = dbQuery.Where(a => a.ChemistId == query.ChemistId && query.date.Date == a.VisitDate);
            }

            return new GetChemistVisitsScheduleQueryResponse()
            {
                ChemistVisitsScheduleDtos = dbQuery.Select(a => new ChemistVisitsScheduleDto
                {
                    ChemistId = a.ChemistId,
                    VisitDate= a.VisitDate,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    PatientAddressId = a.PatientAddressId,
                    PatientId = a.PatientId,
                    PatientName = a.PatientName,
                    VisitId = a.VisitId,
                    VisitCode = a.VisitCode,
                    StatusName = query.CultureName == CultureNames.en? a.StatusNameEn : a.StatusNameAr,
                    AreaName = query.CultureName == CultureNames.en? a.GeoZoneNameEn : a.GeoZoneNameAr,
                    PatientAddress = $"{a.Building} {a.street}, {(query.CultureName == CultureNames.en ? a.GeoZoneNameEn : a.GeoZoneNameAr)}, {(query.CultureName == CultureNames.en ? a.GoverNameEn : a.GeoZoneNameAr)}"

                }).ToList()
            } as IGetChemistVisitsScheduleQueryResponse;
        }
    }
}
