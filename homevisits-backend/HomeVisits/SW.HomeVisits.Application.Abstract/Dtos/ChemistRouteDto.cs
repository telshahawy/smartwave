using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class ChemistDestinationRouteDto
    {
        public Guid ChemistId { get; set; }
        public Guid VisitId { get; set; }
        public TimeSpan? VisitTime { get; set; } //Field found in Visit Table => selected from UI specific Time 
        public float Latitiude { get; set; }
        public float Longitude { get; set; }
        public int Distance { get; set; }
        public int DurationInTraffic { get; set; }
        public int Duration { get; set; }
    }
    public class ChemistRouteDto
    {
        public float StartLatitiude { get; set; }
        public float StartLongitude { get; set; }
        public List<ChemistDestinationRouteDto> Destinations{get;set;}
    }
}
