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
    internal class GetPatientByPatientIdQueryHandler : IQueryHandler<IGetPatientByPatientIdQuery, IGetPatientByPatientIdQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetPatientByPatientIdQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetPatientByPatientIdQueryResponse Read(IGetPatientByPatientIdQuery query)
        {
            IQueryable<PatientSearchView> dbQuery = _context.PatientSearchViews;
            IQueryable<PatientAddressView> dbQueryPatientAddress = _context.PatientAddressViews;
            if (query != null)
            {
                dbQuery = dbQuery.Where(p => p.PatientId == query.PatientId && p.ClientId == query.ClientId);
            }
            var patientAddresses = dbQueryPatientAddress.Where(x => x.PatientId == query.PatientId).ToList();
            return new GetPatientByPatientIdQueryResponse()
            {
                Patient = dbQuery.Select(p => new PatientsDto
                {
                   UserId = p.PatientId,
                   Name = p.Name,
                   BirthDate = p.BirthDate,
                   DOB = p.DOB,
                   PatientAddresses = patientAddresses.Select(x=> new PatientAddressDto
                   {
                       AddressCreatedAt = x.AddressCreatedAt,
                       Building = x.Building,
                       CountryId = x.CountryId,
                       AddressFormatted = "",
                       CountryName = x.CountryNameAr,
                       Flat = x.Flat,
                       Floor = x.Floor,
                       GeoZoneId = x.GeoZoneId,
                       GovernateId = x.GovernateId,
                       GovernateName = x.GoverNameAr,
                       IsConfirmed = x.IsConfirmed,
                       KmlFilePath = x.KmlFilePath,
                       Latitude = x.Latitude,
                       LocationUrl = x.LocationUrl,
                       Longitude = x.Longitude,
                       PatientAddressId = x.PatientAddressId,
                       street = x.street,
                       ZoneName = x.ZoneNameAr,
                       Code=x.Code
                       
                   }).ToList()
                }).FirstOrDefault()
            } as IGetPatientByPatientIdQueryResponse;
        }
    }
}
