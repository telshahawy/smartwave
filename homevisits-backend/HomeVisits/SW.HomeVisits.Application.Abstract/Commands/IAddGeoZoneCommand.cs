using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IAddGeoZoneCommand
    {
        Guid GeoZoneId { get; }
        string NameAr { get; set; }
        string NameEn { get; set; }
        string MappingCode { get; set; }
        string KmlFilePath { get; set; }
        string KmlFileName { get; set; }
        Guid GovernateId { get; set; }
        bool IsActive { get; set; }
        Guid CreatedBy { get; set; }
        public List<CreateTimeZoneFrameDto> TimeZoneFrames { get; set; }

    }
}
