using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetChemistVisitInPermitTimeQueryHandler : IQueryHandler<IGetChemistVisitInPermitTimeQuery, IGetChemistVisitInPermitTimeQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetChemistVisitInPermitTimeQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetChemistVisitInPermitTimeQueryResponse Read(IGetChemistVisitInPermitTimeQuery query)
        {
            if (query == null)
                throw new NullReferenceException(nameof(query));

            var chemistVisits = _context.VisitsViews.Where(p => p.ChemistId.HasValue &&
                p.ChemistId == query.ChemistId &&
                p.ClientId == query.ClientId &&
                p.VisitDate == query.PermitDate &&
                p.VisitStatusTypeId != (int)VisitStatusTypes.Done && p.VisitStatusTypeId != (int)VisitStatusTypes.Cancelled && p.VisitStatusTypeId != (int)VisitStatusTypes.Reject
            );

            List<ChemistVisitInPermitTimeDto> chemistVisitInPermitTime = new List<ChemistVisitInPermitTimeDto>();
            var chemistVisitsTimeZone = (from cv in chemistVisits
                                         join tzf in _context.TimeZoneFramesViews on cv.TimeZoneGeoZoneId equals tzf.TimeZoneFrameId
                                         where query.PermitStartTime <= tzf.StartTime && query.PermitEndTime >= tzf.EndTime
                                         select new ChemistVisitInPermitTimeDto
                                         {
                                             VisitId = cv.VisitId,
                                             VisitNo = cv.VisitNo,
                                             VisitCode = cv.VisitCode,
                                             TimeZoneGeoZoneId = tzf.TimeZoneFrameId,
                                             TimeZoneStartTime = tzf.StartTime,
                                             TimeZoneEndTime = tzf.EndTime,
                                             TimeZoneStartTimeString = new DateTime(tzf.StartTime.Ticks).ToString("hh:mm tt"),
                                             TimeZoneEndTimeString = new DateTime(tzf.EndTime.Ticks).ToString("hh:mm tt"),
                                             TimeZoneTimeSlot = $"{new DateTime(tzf.StartTime.Ticks).ToString("hh:mm tt")}:{new DateTime(tzf.EndTime.Ticks).ToString("hh:mm tt")}",
                                         }).ToList();

            var chemistVisitsInPartOFTimeZone = (from cv in chemistVisits
                                                 join tzf in _context.TimeZoneFramesViews on cv.TimeZoneGeoZoneId equals tzf.TimeZoneFrameId
                                                 where (query.PermitStartTime <= tzf.StartTime && query.PermitEndTime < tzf.EndTime) ||
                                                       (query.PermitStartTime > tzf.StartTime && query.PermitStartTime < tzf.EndTime)
                                                 select new
                                                 {
                                                     VisitId = cv.VisitId,
                                                     VisitNo = cv.VisitNo,
                                                     VisitCode = cv.VisitCode,
                                                     TimeZoneGeoZoneId = tzf.TimeZoneFrameId,
                                                     TimeZoneStartTime = tzf.StartTime,
                                                     TimeZoneEndTime = tzf.EndTime
                                                 }).ToList();

            var systemParameter = _context.SystemParametersViews.FirstOrDefault(p => p.ClientId == query.ClientId);

            var chemistVisitsInPartOFTimeZoneGroup = chemistVisitsInPartOFTimeZone.GroupBy(p => p.TimeZoneGeoZoneId);
            foreach (var item in chemistVisitsInPartOFTimeZoneGroup)
            {
                var chemistVisitEstimatedTime = item.Count() * (systemParameter.EstimatedVisitDurationInMin + systemParameter.RoutingSlotDurationInMin);
                var chemistAvailableMinutesInTimeZone = GetAvailableMinutesInTimeZone(item.FirstOrDefault().TimeZoneStartTime, item.FirstOrDefault().TimeZoneEndTime,
                    query.PermitStartTime, query.PermitEndTime, query.PermitDate, query.ClientId);
                if (chemistVisitEstimatedTime > chemistAvailableMinutesInTimeZone)
                {
                    chemistVisitInPermitTime.AddRange(item.Select(p => new ChemistVisitInPermitTimeDto
                    {
                        VisitId = p.VisitId,
                        VisitNo = p.VisitNo,
                        VisitCode = p.VisitCode,
                        TimeZoneGeoZoneId = p.TimeZoneGeoZoneId,
                        TimeZoneStartTime = p.TimeZoneStartTime,
                        TimeZoneEndTime = p.TimeZoneEndTime,
                        TimeZoneStartTimeString = new DateTime(p.TimeZoneStartTime.Ticks).ToString("hh:mm tt"),
                        TimeZoneEndTimeString = new DateTime(p.TimeZoneEndTime.Ticks).ToString("hh:mm tt"),
                        TimeZoneTimeSlot = $"{new DateTime(p.TimeZoneStartTime.Ticks).ToString("hh:mm tt")}:{new DateTime(p.TimeZoneEndTime.Ticks).ToString("hh:mm tt")}"
                    }));
                }
            }

            if (chemistVisitsTimeZone != null && chemistVisitsTimeZone.Count > 0)
                chemistVisitInPermitTime.AddRange(chemistVisitsTimeZone);

            return new GetChemistVisitInPermitTimeQueryResponse()
            {
                ChemistVisitInPermitTime = chemistVisitInPermitTime
            } as IGetChemistVisitInPermitTimeQueryResponse;
        }

        private int GetAvailableMinutesInTimeZone(TimeSpan timezoneStart, TimeSpan timezoneEnd, TimeSpan chemistPermitStartTime, TimeSpan chemistPermitEndTime, DateTime queryDate, Guid clientId)
        {
            var availableMinues = 0;
            var systemParameter = _context.SystemParametersViews.FirstOrDefault(p => p.ClientId == clientId);

            if ((chemistPermitStartTime >= timezoneStart && chemistPermitStartTime <= timezoneEnd) &&
                (chemistPermitEndTime >= timezoneStart && chemistPermitEndTime <= timezoneEnd))
            {
                var minBeforePermit = (int)(chemistPermitStartTime - timezoneStart).TotalMinutes;
                var minAfterPermit = (int)(timezoneEnd - chemistPermitEndTime).TotalMinutes;

                if (minBeforePermit > systemParameter.EstimatedVisitDurationInMin + systemParameter.RoutingSlotDurationInMin)
                    availableMinues += minBeforePermit;

                if (minAfterPermit > systemParameter.EstimatedVisitDurationInMin + systemParameter.RoutingSlotDurationInMin)
                    availableMinues += minAfterPermit;
            }
            else if (chemistPermitStartTime >= timezoneStart && chemistPermitStartTime <= timezoneEnd && chemistPermitEndTime >= timezoneEnd)
            {
                var minBeforePermit = (int)(chemistPermitStartTime - timezoneStart).TotalMinutes;
                if (minBeforePermit > systemParameter.EstimatedVisitDurationInMin + systemParameter.RoutingSlotDurationInMin)
                    availableMinues += minBeforePermit;
            }
            else if (chemistPermitStartTime <= timezoneStart && chemistPermitEndTime >= timezoneEnd)
            {
                availableMinues = 0;
            }
            else if (chemistPermitStartTime <= timezoneStart && chemistPermitEndTime >= timezoneStart && chemistPermitEndTime <= timezoneEnd)
            {
                var minAfterPermit = (int)(timezoneEnd - chemistPermitEndTime).TotalMinutes;
                if (minAfterPermit > systemParameter.EstimatedVisitDurationInMin + systemParameter.RoutingSlotDurationInMin)
                    availableMinues += minAfterPermit;
            }
            return availableMinues;
        }

    }
}
