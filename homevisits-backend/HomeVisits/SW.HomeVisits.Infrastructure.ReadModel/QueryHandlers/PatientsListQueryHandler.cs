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
    internal class PatientsListQueryHandler : IQueryHandler<IPatientsListQuery, IPatientsListQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public PatientsListQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IPatientsListQueryResponse Read(IPatientsListQuery query)
        {
            IQueryable<PatientsListView> dbQuery = _context.PatientsListViews;
            if (query != null)
            {
                dbQuery = dbQuery.Where(x => x.ClientId == query.ClientId && (string.IsNullOrWhiteSpace(query.PhoneNumber) || x.PhoneNumber == query.PhoneNumber));
            }

            var patients = dbQuery.ToList();
            var patientPhones = patients.GroupJoin(_context.PatientPhoneNumbersViews.AsQueryable(),  //inner sequence
                                          patient => patient.PatientId, //outerKeySelector 
                                          pp => pp.PatientId,     //innerKeySelector
                                          (patient, phonesCollection) => new // resultSelector 
                                          {
                                              patient,
                                              Phones = phonesCollection,
                                          });

            var result = patientPhones.GroupJoin(_context.PatientAddressViews.AsQueryable(),  //inner sequence
                               patient => patient.patient.PatientId, //outerKeySelector 
                               pa => pa.PatientId,     //innerKeySelector
                               (patient, addressCollection) => new // resultSelector 
                               {
                                   patient.patient,
                                   Addresses = addressCollection,
                                   patient.Phones
                               });

            return new PatientsListQueryResponse()
            {
                Patients = result.GroupBy(p => p.patient.PatientId).Select(p => new PatientsDto
                {
                    UserId = p.First().patient.PatientId,
                    Name = p.First().patient.Name,
                    Gender = p.First().patient.Gender,
                    GenderName = query.CultureName == CultureNames.ar ? (p.First().patient.Gender == 1 ? "ذكر" : "انثى") : (p.First().patient.Gender == 1 ? "Male" : "Female"),
                    DOB = p.First().patient.DOB,
                    BirthDate = p.First().patient.BirthDate,
                    PhoneNumber = p.First().Phones.OrderByDescending(x => x.CreatedAt).FirstOrDefault().PhoneNumber,
                    GovernateName = query.CultureName == CultureNames.ar ? p.First().Addresses.OrderByDescending(x => x.AddressCreatedAt).FirstOrDefault().GoverNameAr : p.First().Addresses.OrderByDescending(x => x.AddressCreatedAt).FirstOrDefault().GoverNameEn,
                    PatientAddresses = p.First().Addresses.OrderByDescending(x => x.AddressCreatedAt).Select(pa => new PatientAddressDto
                    {
                        PatientAddressId = pa.PatientAddressId,
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
                        AddressFormatted = string.Format("{0} - {1} - {2} - {3}", pa.Flat, pa.Floor, pa.Building, pa.street),
                        AddressCreatedAt = pa.AddressCreatedAt
                    })
                }).ToList()
            } as IPatientsListQueryResponse;
        }
    }
}
