using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.WebAPI.Models
{
    public class AddGeoZoneCommand : IAddGeoZoneCommand
    {
        public Guid GeoZoneId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string MappingCode { get; set; }
        public string KmlFilePath { get; set; }
        public string KmlFileName { get; set; }
        public Guid GovernateId { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
        public List<CreateTimeZoneFrameDto> TimeZoneFrames { get; set; }
    }
}
