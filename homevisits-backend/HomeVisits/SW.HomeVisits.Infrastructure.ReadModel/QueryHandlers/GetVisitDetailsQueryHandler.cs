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
using SW.Framework.Extensions;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    internal class GetVisitDetailsQueryHandler : IQueryHandler<IGetVisitDetailsQuery, IGetVisitDetailsQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetVisitDetailsQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetVisitDetailsQueryResponse Read(IGetVisitDetailsQuery query)
        {
            IQueryable<VisitDetailsView> dbQuery = _context.VisitDetailsViews;
            IQueryable<GeoZoneView> geoQuery = _context.GeoZoneView;
            IQueryable<UserView> userQuery = _context.UserViews;
            if (query != null)
            {
                dbQuery = dbQuery.Where(v => v.VisitId == query.VisitId);
            }

            var visitDetails = dbQuery.OrderByDescending(vd => vd.ActionCreationDate).ToList();
            var visitAttachments = visitDetails.GroupJoin(_context.VisitAttachmentViews.AsQueryable(),  //inner sequence
                                vd => vd.VisitId, //outerKeySelector 
                                va => va.VisitId,     //innerKeySelector
                                (visitdetail, visitAttachCollection) => new // resultSelector 
                                {
                                    VisitId = visitdetail.VisitId,
                                    VisitNo = visitdetail.VisitNo,
                                    VisitCode = visitdetail.VisitCode,
                                    VisitDate = visitdetail.VisitDate,
                                    CreatedBy = visitdetail.CreatedBy,
                                    PatientId = visitdetail.PatientId,
                                    Name = visitdetail.Name,
                                    Gender = visitdetail.Gender,
                                    DOB = visitdetail.DOB,
                                    RequiredTests = visitdetail.RequiredTests,
                                    Comments = visitdetail.Comments,
                                    GeoZoneId = visitdetail.GeoZoneId,
                                    ZoneNameAr = visitdetail.ZoneNameAr,
                                    ZoneNameEn = visitdetail.ZoneNameEn,
                                    VisitStatusTypeId = visitdetail.VisitStatusTypeId,
                                    StatusNameAr = visitdetail.StatusNameAr,
                                    StatusNameEn = visitdetail.StatusNameEn,
                                    ActionCreationDate = visitdetail.ActionCreationDate,
                                    PlannedNoOfPatients = visitdetail.PlannedNoOfPatients,
                                    Longitude = visitdetail.Longitude,
                                    Latitude = visitdetail.Latitude,
                                    UserType = visitdetail.UserType,
                                    Floor = visitdetail.Floor,
                                    Flat = visitdetail.Flat,
                                    Building = visitdetail.Building,
                                    Street = visitdetail.street,
                                    GoverNameEn = visitdetail.GoverNameEn,
                                    GoverNameAr = visitdetail.GoverNameAr,
                                    TimeZoneGeoZoneId = visitdetail.TimeZoneGeoZoneId,
                                    StartTime = visitdetail.StartTime,
                                    EndTime = visitdetail.EndTime,
                                    VisitAttachments = visitAttachCollection,
                                    VisitTypeId = visitdetail.VisitTypeId,
                                    RelativeAgeSegmentId = visitdetail.RelativeAgeSegmentId,
                                    PatientAddressId = visitdetail.PatientAddressId,
                                    ChemistId = visitdetail.ChemistId,
                                    ChemistName = visitdetail.ChemistName,
                                    StatusCreatedBy = visitdetail.StatusCreatedBy,
                                    VisitTime = visitdetail.VisitTime,
                                    IamNotSure = visitdetail.IamNotSure,
                                    RelativeDateOfBirth = visitdetail.RelativeDateOfBirth
                                });

            var patinetPhones = visitDetails.GroupJoin(_context.PatientPhoneNumbersViews.AsQueryable(),  //inner sequence
                                vd => vd.PatientId, //outerKeySelector 
                                pp => pp.PatientId,     //innerKeySelector
                                (visitDetail, phonesCollection) => new // resultSelector 
                                {
                                    MobileNumber = phonesCollection.OrderBy(x => x.CreatedAt).FirstOrDefault().PhoneNumber
                                });

            var visitStatus = visitDetails.GroupJoin(_context.VisitStatusViews.AsQueryable(),  //inner sequence
                                vd => vd.VisitId, //outerKeySelector 
                                vs => vs.VisitId,     //innerKeySelector
                                (visitDetail, visitStatusCollection) => new // resultSelector 
                                {
                                    VisitStatuses = visitStatusCollection
                                });

            return new GetVisitDetailsQueryResponse()
            {
                VisitDetails = visitAttachments.Select(v => new VisitDetailsDto
                {
                    VisitId = v.VisitId,
                    VisitNo = v.VisitNo,
                    VisitCode = v.VisitCode,
                    VisitDate = v.VisitDate,
                    VisitDateString = v.VisitDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                    VisitTime = v.VisitTime.HasValue ? v.VisitTime.Value.LocalizedTimeFormat(query.CultureName == Application.Abstract.Enum.CultureNames.ar ? new CultureInfo("ar-EG") : new CultureInfo("en-US")) : v.VisitDate.LocalizedTimeFormat(query.CultureName == Application.Abstract.Enum.CultureNames.ar ? new CultureInfo("ar-EG") : new CultureInfo("en-US")),
                    VisitTimeValue = v.VisitTime,
                    CreatedBy = v.CreatedBy,
                    PatientId = v.PatientId,
                    Name = v.Name,
                    ChemistId = v.ChemistId,
                    ChemistName = v.ChemistName,
                    Gender = v.Gender,
                    GenderName = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? (v.Gender == 1 ? "ذكر" : "انثى") : (v.Gender == 1 ? "Male" : "Female"),
                    DOB = v.DOB,
                    RequiredTests = v.RequiredTests,
                    Comments = v.Comments,
                    GeoZoneId = v.GeoZoneId,
                    ZoneName = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? v.ZoneNameAr : v.ZoneNameEn,
                    VisitStatusTypeId = v.VisitStatusTypeId,
                    StatusName = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? v.StatusNameAr : v.StatusNameEn,
                    ActionCreationDate = v.ActionCreationDate,
                    PlannedNoOfPatients = v.PlannedNoOfPatients,
                    PatientMobileNumber = patinetPhones.FirstOrDefault().MobileNumber,
                    Longitude = v.Longitude.GetValueOrDefault(),
                    Latitude = v.Latitude.GetValueOrDefault(),
                    ReservedBy = v.UserType.HasValue ? (query.CultureName == Application.Abstract.Enum.CultureNames.en ? ((UserTypes)v.UserType).ToString() : ((UserTypesAr)v.UserType).ToString()) : string.Empty,
                    Floor = v.Floor,
                    Flat = v.Flat,
                    Building = v.Building,
                    Street = v.Street,
                    PatientAddressId = v.PatientAddressId,
                    //AddressFormatted = string.Format("{0} - {1} - {2} - {3}", v.Flat, v.Floor, v.Building, v.Street),
                    AddressFormatted = $"Street:{v.Street}, Building:{v.Building}, Floor:{v.Floor}, Flat:{v.Flat},{v.GoverNameEn}, ({geoQuery.Where(x => x.GeoZoneId == v.GeoZoneId).FirstOrDefault().NameEn}) ",
                    GoverName = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? v.GoverNameAr : v.GoverNameEn,
                    TimeZoneGeoZoneId = v.TimeZoneGeoZoneId,
                    TimeSlot = $"{new DateTime(v.StartTime.Ticks).ToString("hh:mm tt")}:{new DateTime(v.EndTime.Ticks).ToString("hh:mm tt")}",//$"{new DateTime(timeQuery.Where(x => x.TimeZoneFrameId == v.TimeZoneGeoZoneId).FirstOrDefault().StartTime.Ticks).ToString("hh:mm tt")} : {new DateTime(timeQuery.Where(x => x.TimeZoneFrameId == v.TimeZoneGeoZoneId).FirstOrDefault().EndTime.Ticks).ToString("hh:mm tt")}",
                    IamNotSure = v.IamNotSure,
                    RelativeDateOfBirth = v.RelativeDateOfBirth,
                    StartTime = query.CultureName == CultureNames.en ? new DateTime(v.StartTime.Ticks).ToString("hh:mm tt") : new DateTime(v.StartTime.Ticks).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-EG")),
                    EndTime = query.CultureName == CultureNames.en ? new DateTime(v.EndTime.Ticks).ToString("hh:mm tt") : new DateTime(v.EndTime.Ticks).ToString("hh:mm tt", CultureInfo.CreateSpecificCulture("ar-EG")),
                    VisitTypeId = v.VisitTypeId,
                    RelativeAgeSegmentId = v.RelativeAgeSegmentId,
                    VisitAttachments = v.VisitAttachments.Select(va => new VisitAttachmentsDto
                    {
                        AttachmentId = va.AttachmentId,
                        FileName = va.FileName,
                        VisitId = va.VisitId
                    }),
                    VisitStatuses = visitStatus.FirstOrDefault().VisitStatuses.OrderByDescending(s => s.CreationDate).Select(vs => new VisitStatusesDto
                    {
                        CreationDate = vs.CreationDate,
                        VisitStatusId = vs.VisitStatusId,
                        VisitStatusTypeId = vs.VisitStatusTypeId,
                        Status = query.CultureName == Application.Abstract.Enum.CultureNames.ar ? vs.StatusNameAr : vs.StatusNameEn,
                        UserName = userQuery.Where(x => x.UserId == vs.CreatedBy).FirstOrDefault()?.Name == null ? vs.UserName : userQuery.Where(x => x.UserId == vs.CreatedBy).FirstOrDefault()?.Name,
                        Longitude = vs.Longitude.GetValueOrDefault(),
                        Latitude = vs.Latitude.GetValueOrDefault(),
                        VisitId = vs.VisitId
                    })
                }).FirstOrDefault()
            } as IGetVisitDetailsQueryResponse;
        }
    }
}
