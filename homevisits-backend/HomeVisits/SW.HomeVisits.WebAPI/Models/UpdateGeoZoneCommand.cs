using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateGeoZoneCommand : IUpdateGeoZoneCommand
    {
        public Guid GeoZoneId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public string KmlFileName { get; set; }
        public string MappingCode { get; set; }
        public string KmlFilePath { get; set; }
        public Guid GovernateId { get; set; }
        public List<CreateTimeZoneFrameDto> TimeZoneFrames { get; set; }

    }
}
