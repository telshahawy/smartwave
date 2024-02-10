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
    internal class SearchPatientsQueryHandler : IQueryHandler<ISearchPatientsQuery, ISearchPatientsQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public SearchPatientsQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public ISearchPatientsQueryResponse Read(ISearchPatientsQuery query)
        {
            IQueryable<PatientSearchView> dbQuery = _context.PatientSearchViews;
            IQueryable<GeoZoneView> geoQuery = _context.GeoZoneView;
            if (query != null)
            {
                dbQuery = dbQuery.Where(x => x.PhoneNumber == query.PhoneNumber && x.ClientId == query.ClientId);
            }

            var patients = dbQuery.ToList();
            var groupJoin = patients.GroupJoin(_context.PatientVisitsViews.AsQueryable(),  //inner sequence
                                            patient => patient.PatientId, //outerKeySelector 
                                            pv => pv.PatientId,     //innerKeySelector
                                            (patient, visitsCollection) => new // resultSelector 
                                            {
                                                patient,
                                                Visits = visitsCollection.Where(v => v.VisitStatusTypeId != (int)VisitStatusTypes.Done && v.VisitStatusTypeId != (int)VisitStatusTypes.Cancelled
                                                                                && v.VisitStatusTypeId != (int)VisitStatusTypes.Reject)
                                            });

            var patientPhones = groupJoin.GroupJoin(_context.PatientPhoneNumbersViews.AsQueryable(),  //inner sequence
                                          patient => patient.patient.PatientId, //outerKeySelector 
                                          pp => pp.PatientId,     //innerKeySelector
                                          (patient, phonesCollection) => new // resultSelector 
                                          {
                                              patient.patient,
                                              Phones = phonesCollection,
                                              patient.Visits
                                          });

            var result = patientPhones.GroupJoin(_context.PatientAddressViews.AsQueryable(),  //inner sequence
                                       patient => patient.patient.PatientId, //outerKeySelector 
                                       pa => pa.PatientId,     //innerKeySelector
                                       (patient, addressCollection) => new // resultSelector 
                                       {
                                           patient.patient,
                                           Addresses = addressCollection,
                                           patient.Phones,
                                           patient.Visits
                                       });

            return new SearchPatientsQueryResponse()
            {
                Patients = result.Select(p => new PatientsDto
                {
                    UserId = p.patient.PatientId,
                    Name = p.patient.Name,
                    Gender = p.patient.Gender,
                    GenderName = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? (p.patient.Gender == 1 ? "ذكر" : "انثى") : (p.patient.Gender == 1 ? "Male" : "Female"),
                    DOB = p.patient.DOB,
                    BirthDate = p.patient.BirthDate,
                    PhoneNumber = p.Phones.OrderBy(x => x.CreatedAt).FirstOrDefault().PhoneNumber,
                    IsTherePendingVisits = p.Visits.Where(p => p.VisitStatusTypeId != (int)VisitStatusTypes.Done
                && p.VisitDate >= DateTime.Today).Any(),
                    PendingVistis = p.Visits.Select(v => new PatientVistisDto
                    {

                        VisitId = v.VisitId,
                        VisitNo = v.VisitNo,
                        VisitDate = v.VisitDate,
                        GeoZoneId = v.GeoZoneId,
                        ZoneName = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? v.ZoneNameAr : v.ZoneNameEn,
                        VisitStatusTypeId = v.VisitStatusTypeId,
                        StatusName = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? v.StatusNameAr : v.StatusNameEn,
                        PatientId = v.PatientId,
                        VisitDateString = v.VisitDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                        VisitTime = v.VisitDate.ToString("hh:mm tt", query.CultureName == Application.Abstract.Enum.CultureNames.ar ? new CultureInfo("ar-EG") : new CultureInfo("en-US"))
                    }).Where(p => p.VisitStatusTypeId != (int)VisitStatusTypes.Done
                && p.VisitDate >= DateTime.Today).OrderBy(x=>x.VisitDate),
                    PatientPhoneNumbers = p.Phones.OrderBy(x => x.CreatedAt).Select(pp => new PatientPhoneNumbersDto
                    {
                        PhoneNumber = pp.PhoneNumber,
                        CreatedAt = pp.CreatedAt

                    }),
                    PatientAddresses = p.Addresses.OrderBy(x => x.AddressCreatedAt).Select(pa => new PatientAddressDto
                    {
                        PatientAddressId = pa.PatientAddressId,
                        Code=pa.Code,
                        Latitude = pa.Latitude,
                        Longitude = pa.Longitude,
                        Floor = pa.Floor,
                        Flat = pa.Flat,
                        GeoZoneId = pa.GeoZoneId,
                        Building = pa.Building,
                        street = pa.street,
                        IsConfirmed = pa.IsConfirmed,
                        LocationUrl = pa.LocationUrl,
                        KmlFilePath = pa.KmlFilePath,
                        GovernateId = pa.GovernateId,
                        CountryId = pa.CountryId,
                        ZoneName = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? pa.ZoneNameAr : pa.ZoneNameEn,
                        GovernateName = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? pa.GoverNameAr : pa.GoverNameEn,
                        CountryName = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? pa.CountryNameAr : pa.CountryNameEn,
                        //AddressFormatted = string.Format("{0} - {1} - {2} - {3}",pa.Flat, pa.Floor, pa.Building, pa.street),
                        AddressFormatted=$"Street:{pa.street}, Building:{pa.Building}, Floor:{pa.Floor}, Flat:{pa.Flat},{pa.GoverNameEn}, ({geoQuery.Where(x => x.GeoZoneId == pa.GeoZoneId).FirstOrDefault().NameEn}) ",
                        AddressCreatedAt = pa.AddressCreatedAt
                    })
                }).ToList()
            } as ISearchPatientsQueryResponse;
        }
    }
}
