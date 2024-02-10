using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IUpdateGeoZoneCommand
    {
        Guid GeoZoneId { get; }
        int Code { get; }
        string Name { get; }
        bool IsActive { get; }
        string MappingCode { get; set; }
        string KmlFilePath { get; set; }
        string KmlFileName { get; set; }
        Guid GovernateId { get; set; }
        public List<CreateTimeZoneFrameDto> TimeZoneFrames { get; set; }

    }
}
