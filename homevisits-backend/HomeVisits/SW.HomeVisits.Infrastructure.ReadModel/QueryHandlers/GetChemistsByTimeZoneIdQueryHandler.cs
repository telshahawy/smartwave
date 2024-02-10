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
    internal class GetChemistsByTimeZoneIdQueryHandler : IQueryHandler<IGetChemistsByTimeZoneIdQuery, IGetChemistsByTimeZoneIdQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetChemistsByTimeZoneIdQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetChemistsByTimeZoneIdQueryResponse Read(IGetChemistsByTimeZoneIdQuery query)
        {
            IQueryable<ChemistTimeZoneAvailabilityView> dbQuery = _context.ChemistTimeZoneAvailabilityViews;

            if (query != null)
            {
                dbQuery = dbQuery.Where(a => a.ChemistClientId == query.ClientId && a.TimeZoneFrameId == query.TimeZoneGeoZoneId && query.date >= a.ScheuleStartDate.Date &&
                query.date <= a.ScheduleEndDate.Date && a.Day == DayOfWeek(query.date, System.DayOfWeek.Sunday));
            }

            var availableChemists = dbQuery.ToList().GroupBy(c => c.ChemistId);


            var chemistVisits = availableChemists.GroupJoin(_context.VisitsViews.Where(x => x.VisitDate.Date == query.date.Date && x.TimeZoneGeoZoneId == query.TimeZoneGeoZoneId &&
            x.VisitStatusTypeId != (int)VisitStatusTypes.Done && x.VisitStatusTypeId != (int)VisitStatusTypes.Cancelled && x.VisitStatusTypeId != (int)VisitStatusTypes.Reject
            ).AsQueryable(),  //inner sequence
                                chemist => chemist.Key, //outerKeySelector 
                                cv => cv.ChemistId,     //innerKeySelector
                                (chemist, chemistVisitsCollection) => new // resultSelector 
                                {
                                    ChemistId = chemist.Key,
                                    StartLangitude = chemist.FirstOrDefault().StartLangitude,
                                    StartLatitude = chemist.FirstOrDefault().StartLatitude,
                                    ExpertChemist = chemist.FirstOrDefault().ExpertChemist,
                                    VisitsNoQouta = chemist.FirstOrDefault().VisitsNoQouta,
                                    ChemistVisits = chemistVisitsCollection,
                                    TotalVisits = chemistVisitsCollection.Count(),
                                    ChemistStartTime = chemist.FirstOrDefault().ChemistStartTime,
                                    ChemistEndTime = chemist.FirstOrDefault().ChemistEndTime,
                                });


            return new GetChemistsByTimeZoneIdQueryResponse()
            {
                AvailableChemists = chemistVisits.Select(a => new AvailableChemistsDto
                {
                    ChemistId = a.ChemistId,
                    StartLatitude = a.StartLatitude,
                    StartLangitude = a.StartLangitude,
                    ExpertChemist = a.ExpertChemist,
                    VisitsNoQuota = a.VisitsNoQouta,
                    TotalVisits = a.TotalVisits,
                    ChemistStartTime = a.ChemistStartTime,
                    ChemistEndTime = a.ChemistEndTime,
                    ChemistVisits = a.ChemistVisits.Select(v => new VisitsDto
                    {
                        VisitId = v.VisitId,
                        VisitNo = v.VisitNo,
                        Latitude = v.Latitude,
                        Longitude = v.Longitude,
                        ChemistId = v.ChemistId,
                        GeoZoneId = v.GeoZoneId,
                        VisitDateValue = v.VisitDate
                    }).ToList()
                }).ToList()
            } as IGetChemistsByTimeZoneIdQueryResponse;
        }
        private int DayOfWeek(DateTime value, DayOfWeek firstDayOfWeek)
        {
            var idx = 7 + (int)value.DayOfWeek - (int)firstDayOfWeek;
            if (idx > 6) // week ends at 6, because Enum.DayOfWeek is zero-based
            {
                idx -= 7;
            }
            return idx + 1;
        }
    }
}
