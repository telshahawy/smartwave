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
    public class GetAvailableVisitsInAreaQueryHandler : IQueryHandler<IGetAvailableVisitsInAreaQuery, IGetAvailableVisitsInAreaQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetAvailableVisitsInAreaQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetAvailableVisitsInAreaQueryResponse Read(IGetAvailableVisitsInAreaQuery query)
        {
            IQueryable<ChemistTimeZoneAvailabilityView> dbQuery = _context.ChemistTimeZoneAvailabilityViews.AsQueryable();
            IQueryable<GetAllVisitsView> visitsDbQuery = _context.GetAllVisitsView.AsQueryable();
            IQueryable<OnHoldVisitsView> onHoldVisitsViews = _context.OnHoldVisitsViews.AsQueryable();
            var currentDate = DateTime.Now;
            if (query != null)
            {
                dbQuery = dbQuery.Where(x => x.GeoZoneId == query.GeoZoneId && !x.BranchDispatch && query.date >= x.ScheuleStartDate.Date &&
                    query.date < x.ScheduleEndDate.Date && x.ChemistClientId == query.ClientId && x.Day == DayOfWeek(query.date, System.DayOfWeek.Sunday)
                );

                if (dbQuery == null)
                    throw new Exception("Chemist not found");

                var visitsGroupQuery = visitsDbQuery.Where(x => x.VisitDate.Date == query.date.Date && x.ClientId == query.ClientId && x.GeoZoneId == query.GeoZoneId &&
                /*x.VisitStatus != (int)VisitStatusTypes.Done &&*/ x.VisitStatus != (int)VisitStatusTypes.Cancelled && x.VisitStatus != (int)VisitStatusTypes.Reject).ToList();

                int reseved = visitsGroupQuery.Count() > 0 ? visitsGroupQuery.Count() : 0;

                onHoldVisitsViews = _context.OnHoldVisitsViews.Where(x => x.CreatedAt >= currentDate.AddMinutes(-3) && x.CreatedAt <= currentDate && !x.IsCanceled);
                var systemParameter = _context.SystemParametersViews.FirstOrDefault(p => p.ClientId == query.ClientId);
                if (dbQuery.Count() > 0)
                {
                    if (visitsGroupQuery == null)
                        throw new Exception("Chemist not found");

                    if (reseved > 0)
                    {
                        var ZoneAvailability = dbQuery.ToList().GroupBy(x => x.TimeZoneFrameId).Select(x => new
                        {
                            ChemistAssignedGeoZoneId = x.FirstOrDefault().ChemistAssignedGeoZoneId,
                            GeoZoneId = x.FirstOrDefault().GeoZoneId,
                            TimeFrameId = x.Key,
                            GeoZoneName = query.CultureName == CultureNames.ar ? x.FirstOrDefault().GeoZoneNameAe : x.FirstOrDefault().GeoZoneNameEn,
                            TimeZoneStartTime = x.FirstOrDefault().TimeZoneStartTime,
                            TimeZoneEndTime = x.FirstOrDefault().TimeZoneEndTime,
                            TimeZoneName = query.CultureName == CultureNames.ar ? x.FirstOrDefault().TimeZoneFrameNameAr : x.FirstOrDefault().TimeZoneFrameNameEn,
                            MaxChemistTime = x.Sum(c => c.ChemistSupportedTime /*/ 45*/),
                            TotalChemistTime = x.Sum(c => GetChemistAvailableTimeInMinutes(c.TimeZoneStartTime, c.TimeZoneEndTime, c.ChemistStartTime, c.ChemistEndTime, query.date) /*/ 45*/),
                            TotalReservedVisits = (visitsGroupQuery.Any() ? visitsGroupQuery.Where(g => x.Select(xs => xs.TimeZoneFrameId)
                                   .Contains(g.TimeZoneGeoZoneId)).Sum(v => v.PlannedNoOfPatients) : 0) 
                                   + onHoldVisitsViews.Count(g => g.TimeZoneFrameId == x.FirstOrDefault().TimeZoneFrameId),
                            TimeZoneFrameGeoZoneId = x.FirstOrDefault().TimeZoneFrameId,
                            TimeZoneQouta = x.FirstOrDefault().VisitsNoQouta * x.Select(c => c.ChemistId).Distinct().Count(),
                            IsAvialableTimeZone = x.Any(p => IsAvialableTimeZone(p, query.date, visitsGroupQuery, systemParameter))
                        }
                        ).OrderBy(x => x.TimeZoneStartTime).ToList();

                        return new GetAvailableVisitsInAreaQueryResponse()
                        {
                            AvailableVisits = ZoneAvailability.Where(x => x.IsAvialableTimeZone).Select(x => new AvailableVisitsInAreaDto
                            {
                                GeoZoneName = x.GeoZoneName,
                                AvalailableVisits = x.TimeZoneQouta - visitsGroupQuery.Where(g => g.TimeZoneFrameId == x.TimeZoneFrameGeoZoneId).Count(),
                                TimeZoneName = x.TimeZoneName,
                                MaxVisits = x.TimeZoneQouta,
                                TimeZoneStartTime = query.CultureName == CultureNames.en ? new DateTime(x.TimeZoneStartTime.Ticks).ToString("hh:mm tt") : new DateTime(x.TimeZoneStartTime.Ticks).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-EG")),
                                TimeZoneEndTime = query.CultureName == CultureNames.en ? new DateTime(x.TimeZoneEndTime.Ticks).ToString("hh:mm tt") : new DateTime(x.TimeZoneEndTime.Ticks).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-EG")),
                                TimeZoneFrameGeoZoneId = x.TimeZoneFrameGeoZoneId,
                                StartTimeSpan = x.TimeZoneStartTime,
                                EndTimeSpan = x.TimeZoneEndTime,
                                GeoZoneId = x.GeoZoneId
                            }).ToList()
                        } as IGetAvailableVisitsInAreaQueryResponse;
                    }
                    else
                    {
                        var ZoneAvailability = dbQuery.ToList().GroupBy(x => x.TimeZoneFrameId).Select(x => new
                        {
                            ChemistAssignedGeoZoneId = x.FirstOrDefault().ChemistAssignedGeoZoneId,
                            GeoZoneId = x.FirstOrDefault().GeoZoneId,
                            TimeFrameId = x.Key,
                            GeoZoneName = query.CultureName == CultureNames.ar ? x.FirstOrDefault().GeoZoneNameAe : x.FirstOrDefault().GeoZoneNameEn,
                            TimeZoneStartTime = x.FirstOrDefault().TimeZoneStartTime,
                            TimeZoneEndTime = x.FirstOrDefault().TimeZoneEndTime,
                            TimeZoneName = query.CultureName == CultureNames.ar ? x.FirstOrDefault().TimeZoneFrameNameAr : x.FirstOrDefault().TimeZoneFrameNameEn,
                            MaxChemistTime = x.Sum(c => c.ChemistSupportedTime /*/ 45*/),
                            TotalChemistTime = x.Sum(c => GetChemistAvailableTimeInMinutes(c.TimeZoneStartTime, c.TimeZoneEndTime, c.ChemistStartTime, c.ChemistEndTime, query.date) /*/ 45*/),
                            TotalReservedVisits = (visitsGroupQuery.Any() ?
                                visitsGroupQuery.Where(g => x.Select(xs => xs.TimeZoneFrameId)
                                .Contains(g.TimeZoneGeoZoneId)).Sum(v => v.PlannedNoOfPatients) : 0)
                                + onHoldVisitsViews.Count(g => g.TimeZoneFrameId == x.FirstOrDefault().TimeZoneFrameId),
                            TimeZoneFrameGeoZoneId = x.FirstOrDefault().TimeZoneFrameId,
                            TimeZoneQouta = x.FirstOrDefault().VisitsNoQouta * x.Select(c => c.ChemistId).Distinct().Count(),
                            IsAvialableTimeZone = x.Any(p => IsAvialableTimeZone(p, query.date, visitsGroupQuery, systemParameter))
                        }).OrderBy(x => x.TimeZoneStartTime).ToList();
                        return new GetAvailableVisitsInAreaQueryResponse()
                        {

                            AvailableVisits = ZoneAvailability.Where(x => x.IsAvialableTimeZone).Select(x => new AvailableVisitsInAreaDto
                            {
                                GeoZoneName = x.GeoZoneName,
                                AvalailableVisits = x.TimeZoneQouta - visitsGroupQuery.Where(g => g.TimeZoneGeoZoneId == x.TimeZoneFrameGeoZoneId).Count(),
                                TimeZoneName = x.TimeZoneName,
                                MaxVisits = x.TimeZoneQouta,
                                TimeZoneStartTime = query.CultureName == CultureNames.en ? new DateTime(x.TimeZoneStartTime.Ticks).ToString("hh:mm tt") : new DateTime(x.TimeZoneStartTime.Ticks).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-EG")),
                                TimeZoneEndTime = query.CultureName == CultureNames.en ? new DateTime(x.TimeZoneEndTime.Ticks).ToString("hh:mm tt") : new DateTime(x.TimeZoneEndTime.Ticks).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-EG")),
                                TimeZoneFrameGeoZoneId = x.TimeZoneFrameGeoZoneId,
                                StartTimeSpan = x.TimeZoneStartTime,
                                EndTimeSpan = x.TimeZoneEndTime,
                                GeoZoneId = x.GeoZoneId
                            }).ToList()
                        } as IGetAvailableVisitsInAreaQueryResponse;
                    }
                }
            }

            return new GetAvailableVisitsInAreaQueryResponse()
            {
                AvailableVisits = new List<AvailableVisitsInAreaDto>()
            } as IGetAvailableVisitsInAreaQueryResponse;
        }

        private bool IsAvialableTimeZone(ChemistTimeZoneAvailabilityView chemistTimeZoneAvailabilityView, DateTime visitDate, List<GetAllVisitsView> allVisitsView, SystemParametersView systemParameter)
        {
            bool result = false;
            //Validate time zone with chemist permit
            int avialableTimeInMinutesAfterPermits;

            var isAvialableTimeZoneWithChemistPermit = IsAvialableTimeZoneWithChemistPermit(chemistTimeZoneAvailabilityView.ChemistClientId,
                chemistTimeZoneAvailabilityView.ChemistId, visitDate, chemistTimeZoneAvailabilityView.TimeZoneStartTime,
                chemistTimeZoneAvailabilityView.TimeZoneEndTime, systemParameter, out avialableTimeInMinutesAfterPermits);

            if (!isAvialableTimeZoneWithChemistPermit)
                return false;

            var currentTime = DateTime.Now.TimeOfDay;
            var currentDate = DateTime.Now.Date;
            var currentDateTime = DateTime.Now;
            if (currentDate == visitDate && currentTime >= chemistTimeZoneAvailabilityView.TimeZoneStartTime && currentTime < chemistTimeZoneAvailabilityView.TimeZoneEndTime)
            {
                var chemistVisitsInTimeZone = allVisitsView.Where(p => p.ChemistId.HasValue && p.ChemistId.Value == chemistTimeZoneAvailabilityView.ChemistId
                    && p.TimeZoneFrameId == chemistTimeZoneAvailabilityView.TimeZoneFrameId).ToList();
                var chemistVisitsCreatedBeforeTimeZoneStart = chemistVisitsInTimeZone.Where(p => p.CreatedDate.TimeOfDay <= chemistTimeZoneAvailabilityView.TimeZoneStartTime).ToList();
                if (chemistVisitsCreatedBeforeTimeZoneStart.Count <= chemistVisitsCreatedBeforeTimeZoneStart.Where(p => p.VisitStatus == (int)VisitStatusTypes.Done).Count())
                {
                    var chemistAvailableTimeInMinutes = GetChemistAvailableTimeInMinutes(chemistTimeZoneAvailabilityView.TimeZoneStartTime, chemistTimeZoneAvailabilityView.TimeZoneEndTime, chemistTimeZoneAvailabilityView.ChemistStartTime, chemistTimeZoneAvailabilityView.ChemistEndTime, visitDate);
                    if (chemistAvailableTimeInMinutes >= systemParameter.EstimatedVisitDurationInMin + systemParameter.RoutingSlotDurationInMin)
                    {
                        var numOfAdditionalVisitAvialable = chemistAvailableTimeInMinutes / (systemParameter.EstimatedVisitDurationInMin + systemParameter.RoutingSlotDurationInMin);
                        var chemistVisitCreatedAfterTimeZoneStart = chemistVisitsInTimeZone.Where(p => p.CreatedDate.TimeOfDay > chemistTimeZoneAvailabilityView.TimeZoneStartTime).ToList();
                        if (chemistVisitCreatedAfterTimeZoneStart.Count < numOfAdditionalVisitAvialable)
                            result = true;
                    }
                }
            }
            else if (currentDate == visitDate && currentTime >= chemistTimeZoneAvailabilityView.TimeZoneStartTime && currentTime >= chemistTimeZoneAvailabilityView.TimeZoneEndTime)
                result = false;
            else
                result = true;

            return result;
        }

        private bool IsAvialableTimeZoneWithChemistPermit(Guid clientId, Guid chemistId, DateTime permitDate, TimeSpan timeZoneStartTime, TimeSpan timeZoneEndTime, SystemParametersView systemParameter, out int avialableTimeInMinutesAfterPermits)
        {
            var totalTimeZoneMinutes = (int)(timeZoneEndTime - timeZoneStartTime).TotalMinutes;
            var chemistPermits = _context.ChemistPermitsViews.Where(x => !x.IsDeleted && x.ClientId == clientId && x.ChemistId == chemistId &&
                 x.PermitDate.Date == permitDate.Date).ToList();

            if (chemistPermits == null || chemistPermits.Count == 0)
                avialableTimeInMinutesAfterPermits = totalTimeZoneMinutes;
            else
            {
                if (chemistPermits.Any(p => p.StartTime <= timeZoneStartTime && p.EndTime >= timeZoneEndTime))
                    avialableTimeInMinutesAfterPermits = 0;
                else if (chemistPermits.All(p => p.StartTime >= timeZoneEndTime || p.EndTime <= timeZoneStartTime))
                    avialableTimeInMinutesAfterPermits = totalTimeZoneMinutes;
                else
                {
                    chemistPermits = chemistPermits.OrderBy(p => p.StartTime).ToList();
                    var updatedTimeZoneStartTime = timeZoneStartTime;
                    foreach (var item in chemistPermits)
                    {
                        if (!(item.StartTime >= timeZoneEndTime || item.EndTime <= timeZoneStartTime))
                        {
                            if ((item.StartTime >= updatedTimeZoneStartTime && item.StartTime <= timeZoneEndTime) &&
                                            (item.EndTime >= updatedTimeZoneStartTime && item.EndTime <= timeZoneEndTime))
                            {
                                var minBeforePermit = (int)(item.StartTime - updatedTimeZoneStartTime).TotalMinutes;
                                var minAfterPermit = (int)(timeZoneEndTime - item.EndTime).TotalMinutes;

                                if (minBeforePermit < systemParameter.EstimatedVisitDurationInMin + systemParameter.RoutingSlotDurationInMin)
                                    totalTimeZoneMinutes -= minBeforePermit;

                                if (minAfterPermit < systemParameter.EstimatedVisitDurationInMin + systemParameter.RoutingSlotDurationInMin)
                                    totalTimeZoneMinutes -= minAfterPermit;

                                totalTimeZoneMinutes -= (int)(item.EndTime - item.StartTime).TotalMinutes;
                            }
                            else if (item.StartTime >= updatedTimeZoneStartTime && item.StartTime <= timeZoneEndTime && item.EndTime >= timeZoneEndTime)
                            {
                                var minBeforePermit = (int)(item.StartTime - updatedTimeZoneStartTime).TotalMinutes;
                                if (minBeforePermit < systemParameter.EstimatedVisitDurationInMin + systemParameter.RoutingSlotDurationInMin)
                                    totalTimeZoneMinutes -= minBeforePermit;

                                totalTimeZoneMinutes -= (int)(timeZoneEndTime - item.StartTime).TotalMinutes;
                            }
                            else if (item.StartTime <= updatedTimeZoneStartTime && item.EndTime >= updatedTimeZoneStartTime && item.EndTime <= timeZoneEndTime)
                            {
                                var minAfterPermit = (int)(timeZoneEndTime - item.EndTime).TotalMinutes;
                                if (minAfterPermit < systemParameter.EstimatedVisitDurationInMin + systemParameter.RoutingSlotDurationInMin)
                                    totalTimeZoneMinutes -= minAfterPermit;

                                totalTimeZoneMinutes -= (int)(item.EndTime - updatedTimeZoneStartTime).TotalMinutes;
                            }
                            updatedTimeZoneStartTime = item.EndTime;
                        }
                    }
                    avialableTimeInMinutesAfterPermits = totalTimeZoneMinutes;
                }
            }

            if (avialableTimeInMinutesAfterPermits < systemParameter.EstimatedVisitDurationInMin + systemParameter.RoutingSlotDurationInMin)
                return false;
            else
                return true;
        }


        private int GetChemistAvailableTimeInMinutes(TimeSpan timezoneStart, TimeSpan timezoneEnd, TimeSpan chemistStartTime, TimeSpan chemistEndTime, DateTime queryDate)
        {
            var availableMinues = 0;
            var currentTime = DateTime.Now.TimeOfDay;
            var currentDate = DateTime.Now.Date;
            //if (currentTime >= timezoneStart)
            //{
            //    availableMinues = 0;
            //}
            if ((chemistStartTime >= timezoneStart && chemistStartTime <= timezoneEnd) &&
                (chemistEndTime >= timezoneStart && chemistEndTime <= timezoneEnd))
            {
                TimeSpan span = currentDate == queryDate && currentTime > chemistStartTime ? chemistEndTime - currentTime : chemistEndTime - chemistStartTime;
                availableMinues = span.TotalMinutes > 0 ? (int)span.TotalMinutes : 0;
            }
            else if (chemistStartTime >= timezoneStart && chemistStartTime <= timezoneEnd && chemistEndTime >= timezoneEnd)
            {
                TimeSpan span = currentDate == queryDate && currentTime > chemistStartTime ? timezoneEnd - currentTime : timezoneEnd - chemistStartTime;
                availableMinues = span.TotalMinutes > 0 ? (int)span.TotalMinutes : 0;
            }
            else if (chemistStartTime <= timezoneStart && chemistEndTime >= timezoneEnd)
            {
                TimeSpan span = currentDate == queryDate && currentTime > timezoneStart ? timezoneEnd - currentTime : timezoneEnd - timezoneStart;
                availableMinues = span.TotalMinutes > 0 ? (int)span.TotalMinutes : 0;
            }
            else if (chemistEndTime >= timezoneStart && chemistEndTime <= timezoneEnd)
            {
                TimeSpan span = currentDate == queryDate && currentTime > timezoneStart ? chemistEndTime - currentTime : chemistEndTime - timezoneStart;
                availableMinues = span.TotalMinutes > 0 ? (int)span.TotalMinutes : 0;
            }
            return availableMinues;
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
