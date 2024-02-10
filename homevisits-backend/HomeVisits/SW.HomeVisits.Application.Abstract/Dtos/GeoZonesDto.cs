using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class GeoZonesDto
    {
        public Guid GeoZoneId { get; set; }
        public int Code { get; set; }
        public string GeoZoneName { get; set; }
        public string MappingCode { get; set; }
        public Guid GovernateId { get; set; }
        public string GovernateName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Guid CountryId { get; set; }
        public string KmlFilePath { get; set; }
        public string KmlFileName { get; set; }

        public IEnumerable<TimeZoneFramesDto> TimeZoneFrames { get; set; }
    }
}