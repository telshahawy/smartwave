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
using SW.HomeVisits.Application.Abstract.GmapServices;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    internal class SearchMyScheduleQueryHandler : IQueryHandler<ISearchMyScheduleQuery, ISearchMyScheduleQueryResponse>
    {
        private readonly ILog _log;
        private readonly IGmapService _gmapService;
        private readonly HomeVisitsReadModelContext _context;

        public SearchMyScheduleQueryHandler(HomeVisitsReadModelContext context, IGmapService gmapService, ILog log)
        {
            _log = log;
            _context = context;
            _gmapService = gmapService;
        }

        public ISearchMyScheduleQueryResponse Read(ISearchMyScheduleQuery query)
        {
            IQueryable<ChemistScheduleView> dbQuery = _context.ChemistScheduleViews;
            IQueryable<GeoZoneView> geoQuery = _context.GeoZoneView;
            IQueryable<TimeZoneFramesView> timeQuery = _context.TimeZoneFramesViews;
            var systemParametersViews = _context.SystemParametersViews.FirstOrDefault(p => p.ClientId == query.ClientId);
            if (query != null)
            {
                dbQuery = dbQuery.Where(x => x.ChemistId == query.ChemistId && x.ClientId == query.ClientId && x.VisitStatusTypeId != (int)VisitStatusTypes.Done &&
                x.VisitStatusTypeId != (int)VisitStatusTypes.Cancelled && x.VisitStatusTypeId != (int)VisitStatusTypes.Reject).OrderBy(o => o.VisitDate);

                //Sorting
                if (query.Order.ToUpper() == "DESC")
                {
                    dbQuery = dbQuery.OrderByDescending(o => o.VisitDate);
                }

                //Filter
                if (!string.IsNullOrEmpty(query.VisitDate))
                {
                    DateTime datetime = Convert.ToDateTime(query.VisitDate);
                    if (datetime.TimeOfDay != TimeSpan.Zero) //comparison with 00:00:00 time
                    {
                        dbQuery = dbQuery.Where(v => v.VisitDate == datetime);
                    }
                    else
                    {
                        DateTime plusOneDateTime = datetime.AddDays(1).Date;
                        dbQuery = dbQuery.Where(x => x.VisitDate >= datetime.Date && x.VisitDate < plusOneDateTime);
                    }
                }
            }

            var patinetPhones = dbQuery.ToList().GroupJoin(_context.PatientPhoneNumbersViews.AsQueryable(),  //inner sequence
                                patient => patient.PatientId, //outerKeySelector 
                                pp => pp.PatientId,     //innerKeySelector
                                (patient, phonesCollection) => new // resultSelector 
                                {
                                    ChemistId = patient.ChemistId,
                                    VisitId = patient.VisitId,
                                    Name = patient.Name,
                                    Gender = patient.Gender,
                                    DOB = patient.DOB,
                                    Street = patient.street,
                                    Longitude = patient.Longitude,
                                    Latitude = patient.Latitude,
                                    VisitStatusTypeId = patient.VisitStatusTypeId,
                                    VisitNo = patient.VisitNo,
                                    VisitDate = patient.VisitDate,
                                    Floor = patient.Floor,
                                    Flat = patient.Flat,
                                    Building = patient.Building,
                                    StatusNameAr = patient.StatusNameAr,
                                    StatusNameEn = patient.StatusNameEn,
                                    ZoneNameAr = patient.ZoneNameAr,
                                    ZoneNameEn = patient.ZoneNameEn,
                                    GoverNameAr = patient.GoverNameAr,
                                    GoverNameEn = patient.GoverNameEn,
                                    PatientId = patient.PatientId,
                                    MobileNumber = phonesCollection.OrderBy(x => x.CreatedAt).FirstOrDefault().PhoneNumber,
                                    ExpertChemist = patient.ExpertChemist,
                                    ClientId = patient.ClientId,
                                    GeoZoneId = patient.GeoZoneId,
                                    TimeZoneGeoZoneId = patient.TimeZoneGeoZoneId,
                                    StartLangitude = patient.ChemistStartLangitude,
                                    StartLatitude = patient.ChemistStartLatitude,
                                    TimeZoneStartTime = patient.TimeZoneStartTime,
                                    TimeZoneEndTime = patient.TimeZoneEndTime,
                                    VisitOrder = patient.VisitOrder,
                                    VisitStartLatitude = patient.VisitStartLatitude,
                                    VisitStartLangitude = patient.VisitStartLangitude,
                                    VisitLatitude = patient.VisitLatitude,
                                    VisitLongitude = patient.VisitLongitude,
                                    VisitDistance = patient.VisitDistance,
                                    VisitDuration = patient.VisitDuration,
                                    VisitDurationInTraffic = patient.VisitDurationInTraffic,
                                    VisitTimeInOrder = patient.VisitOrder.HasValue ? new DateTime(patient.TimeZoneStartTime.Ticks).AddMinutes(((patient.VisitOrder.Value - 1) * systemParametersViews.EstimatedVisitDurationInMin) + (patient.VisitDurationInTraffic.Value / 60)).ToString("hh:mm tt") : string.Empty
                                }).ToList();


            var mySchedule = patinetPhones.OrderBy(p => p.TimeZoneStartTime).ThenBy(p => p.VisitOrder).Select(s => new MyScheduleDto
            {
                ChemistId = s.ChemistId,
                VisitId = s.VisitId,
                Name = s.Name,
                Gender = s.Gender,
                GenderName = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? (s.Gender == 1 ? "ذكر" : "انثى") : (s.Gender == 1 ? "Male" : "Female"),
                DOB = s.DOB,
                MobileNumber = s.MobileNumber,
                Street = s.Street,
                Longitude = s.Longitude,
                Latitude = s.Latitude,
                VisitStatusTypeId = s.VisitStatusTypeId,
                StatusName = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? s.StatusNameAr : s.StatusNameEn,
                VisitNo = s.VisitNo,
                VisitDate = s.VisitDate,
                Zone = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? s.ZoneNameAr : s.ZoneNameEn,
                GoverName = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? s.GoverNameAr : s.GoverNameEn,
                Floor = s.Floor,
                Flat = s.Flat,
                Building = s.Building,
                PatientId = s.PatientId,
                VisitDateString = s.VisitDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                //VisitTime = s.VisitDate.ToString("hh:mm tt", query.CultureName == Application.Abstract.Enum.CultureNames.ar ? new CultureInfo("ar-EG") : new CultureInfo("en-US")),
                //AddressFormatted = string.Format("{0} - {1} - {2} - {3}", s.Flat, s.Floor, s.Building, s.Street),
                AddressFormatted = $"Street:{s.Street}, Building:{s.Building}, Floor:{s.Floor}, Flat:{s.Flat},{s.GoverNameEn}, ({geoQuery.Where(x => x.GeoZoneId == s.GeoZoneId).FirstOrDefault().NameEn})",
                ExpertChemist = s.ExpertChemist,
                ClientId = s.ClientId,
                VisitTime = string.IsNullOrEmpty(s.VisitTimeInOrder) ? $"{new DateTime(s.TimeZoneStartTime.Ticks):hh:mm tt} : {new DateTime(s.TimeZoneEndTime.Ticks):hh:mm tt}" : s.VisitTimeInOrder
            }).ToList();

            //if (patinetPhones != null && patinetPhones.Count() > 1)
            //{
            //    //var timeZoneFrames = timeQuery.Where(p => p.GeoZoneId == patinetPhones.FirstOrDefault().GeoZoneId).OrderBy(p => p.StartTime);
            //    //var patinetPhonesGroup = patinetPhones.OrderBy(p => p.VisitDate).ThenBy(p => p.TimeZoneGeoZoneId).ThenBy(p => p.TimeZoneStartTime).GroupBy(p => p.TimeZoneGeoZoneId);
            //    mySchedule = SortVisites(patinetPhones.FirstOrDefault().ChemistId, patinetPhones.FirstOrDefault().StartLangitude, patinetPhones.FirstOrDefault().StartLatitude, mySchedule);
            //}

            return new SearchMyScheduleQueryResponse()
            {
                mySchedule = mySchedule,
            } as ISearchMyScheduleQueryResponse;
        }

        private List<MyScheduleDto> SortVisites(Guid chemistId, float startLangitude, float startLatitude, List<MyScheduleDto> myScheduleDtos)
        {
            var map = _gmapService.GetDistanceMatrix(new GmapRoutingInputsDto
            {
                Origins = startLatitude.ToString() + "," + startLangitude.ToString(),
                Destinations = myScheduleDtos.Select(x => (x.Latitude + "," + x.Longitude)).ToList()
            }).GetAwaiter().GetResult();

            var routes = new GetChemistRoutesQueryResponse()
            {
                Route = new ChemistRouteDto
                {
                    StartLatitiude = startLatitude,
                    StartLongitude = startLangitude,
                    Destinations = new List<ChemistDestinationRouteDto>()
                }
            };

            for (int i = 0; i < myScheduleDtos.Count(); i++)
            {
                routes.Route.Destinations.Add(new ChemistDestinationRouteDto
                {
                    ChemistId = chemistId,
                    Latitiude = float.Parse(!string.IsNullOrEmpty(myScheduleDtos[i].Latitude) ? myScheduleDtos[i].Latitude : "0"),
                    Longitude = float.Parse(!string.IsNullOrEmpty(myScheduleDtos[i].Longitude) ? myScheduleDtos[i].Longitude : "0"),
                    VisitId = myScheduleDtos[i].VisitId,
                    Distance = map.rows.First().elements[i].distance?.value ?? 0
                });
            }
            routes.Route.Destinations = routes.Route.Destinations.OrderBy(x => x.Distance).ToList();

            List<MyScheduleDto> result = new List<MyScheduleDto>();
            foreach (var visitDestinations in routes.Route.Destinations)
            {
                result.Add(myScheduleDtos.FirstOrDefault(p => p.VisitId == visitDestinations.VisitId));
            }
            return result;
        }
    }
}
