using System;
using System.Linq;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Domain.Enums;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetPatientForEditQueryHandler : IQueryHandler<IGetPatientForEditQuery, IGetPatientForEditQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetPatientForEditQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetPatientForEditQueryResponse Read(IGetPatientForEditQuery query)
        {
            IQueryable<GetPatientsListView> dbQuery = _context.GetPatientsListViews;
            if (query != null)
            {
                dbQuery = dbQuery.Where(x => x.PatientId == query.PatientId);
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


            return new GetPatientForEditQueryResponse
            {
                Patients = result.Select(p => new PatientsDto
                {
                    UserId = p.patient.PatientId,
                    Name = p.patient.Name,
                    Gender = p.patient.Gender,
                    GenderName= p.patient.Gender == (int)GenderTypes.Male ? "Male" : p.patient.Gender == (int)GenderTypes.Female ? "Female" : "UnKnown",
                    DOB = p.patient.DOB,
                    BirthDate = p.patient.BirthDate,
                    PhoneNumber = p.Phones.OrderByDescending(x => x.CreatedAt).FirstOrDefault().PhoneNumber,
                    PatientAddresses = p.Addresses?.OrderByDescending(x => x.AddressCreatedAt).Select(pa => new PatientAddressDto
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
                        AddressFormatted = string.Format("{0} - {1} - {2} - {3}", pa.Flat, pa.Floor, pa.Building, pa.street),
                        AddressCreatedAt = pa.AddressCreatedAt
                    }),
                    PatientPhoneNumbers = p.Phones?.OrderByDescending(x => x.CreatedAt).Select(pp => new PatientPhoneNumbersDto
                    {
                        PhoneNumber = pp.PhoneNumber,
                        CreatedAt = pp.CreatedAt

                    })
                }).FirstOrDefault()

            } as IGetPatientForEditQueryResponse;
        }
    }
}

