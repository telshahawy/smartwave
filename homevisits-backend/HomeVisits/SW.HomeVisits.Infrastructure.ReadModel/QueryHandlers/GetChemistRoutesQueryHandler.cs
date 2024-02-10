using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.GmapServices;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetChemistRoutesQueryHandler : IQueryHandler<IGetChemistRoutesQuery, IGetChemistRoutesQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;
        private readonly IGmapService _gmapService;

        public GetChemistRoutesQueryHandler(HomeVisitsReadModelContext context, ILog log, IGmapService gmapService)
        {
            _context = context;
            _log = log;
            _gmapService = gmapService;
        }

        public IGetChemistRoutesQueryResponse Read(IGetChemistRoutesQuery query)
        {

            IQueryable<ChemistTimeZoneAvailabilityView> dbQuery =
                _context.ChemistTimeZoneAvailabilityViews.AsQueryable();
            IQueryable<VisitsView> visitsDbQuery = _context.VisitsViews.AsQueryable();
            var currentDate = DateTime.Now;
            if (query != null)
            {
                var timezones = dbQuery.Where(x => x.ChemistId == query.ChemistId && !x.BranchDispatch &&
                                                   currentDate.Date >= x.ScheuleStartDate &&
                                                   x.TimeZoneStartTime <= currentDate.TimeOfDay &&
                                                   x.TimeZoneEndTime > currentDate.TimeOfDay &&
                                                   currentDate.Date < x.ScheduleEndDate &&
                                                   x.ChemistClientId == query.ClientId &&
                                                   x.Day == DayOfWeek(currentDate.Date, System.DayOfWeek.Sunday))
                    .ToList().GroupBy(x => x.TimeZoneFrameId);

                if (timezones.Any())
                {
                    var visitsGroupQuery = visitsDbQuery.Where(x => x.VisitDate.Date == currentDate.Date &&
                                                          x.ClientId == query.ClientId &&
                                                          x.ChemistId == query.ChemistId &&
                                                          x.VisitStatusTypeId != (int)VisitStatusTypes.Done &&
                                                          x.VisitStatusTypeId !=
                                                          (int)VisitStatusTypes.Cancelled &&
                                                          x.VisitStatusTypeId != (int)VisitStatusTypes.Reject &&
                                                          timezones.Select(c => c.Key)
                                                              .Contains(x.TimeZoneGeoZoneId))
              .ToList();
                    if (visitsGroupQuery.Any())
                    {
                        var map = _gmapService.GetDistanceMatrix(new GmapRoutingInputsDto
                        {
                            Origins = query.StartLatitude.GetValueOrDefault().ToString() + "," + query.StartLongitude.GetValueOrDefault().ToString(),
                            Destinations = visitsGroupQuery.Select(x => (x.Latitude + "," + x.Longitude)).ToList()

                        }).GetAwaiter().GetResult();
                        var routes = new GetChemistRoutesQueryResponse()
                        {
                            Route = new ChemistRouteDto
                            {
                                StartLatitiude = query.StartLatitude.GetValueOrDefault(),
                                StartLongitude = query.StartLongitude.GetValueOrDefault(),
                                Destinations = new List<ChemistDestinationRouteDto>()
                            }
                        };
                        for(int i = 0; i < visitsGroupQuery.Count(); i++)
                        {
                            routes.Route.Destinations.Add(new ChemistDestinationRouteDto
                            {
                                Latitiude = float.Parse(visitsGroupQuery[i].Latitude),
                                Longitude = float.Parse(visitsGroupQuery[i].Longitude),
                                VisitId = visitsGroupQuery[i].VisitId,
                                Distance = map.rows.First().elements[i].distance.value
                            });
                        }
                        routes.Route.Destinations = routes.Route.Destinations.OrderBy(x => x.Distance).ToList();
                        return routes as IGetChemistRoutesQueryResponse;
                    }
                    
                }
          
            }

            return new GetChemistRoutesQueryResponse()
            {
                Route = new ChemistRouteDto
                {
                    Destinations = new List<ChemistDestinationRouteDto>()
                }
            } as IGetChemistRoutesQueryResponse;
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
            else if (chemistStartTime >= timezoneStart && chemistEndTime >= timezoneEnd)
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
