using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class GMapRoutingDto
    {
        public List<string> destination_addresses { get; set; }
        public List<string> origin_addresses { get; set; }
        public List<GmapRowDto> rows { get; set; }
        public string status { get; set; }
    }
    public class GmapRoutingInputsDto
    {
        public string Origins { get; set; }
        public List<string> Destinations { get; set; }
    }
    public class GmapRowDto
    {
        public List<GmapElementDto> elements { get; set; }
    }
    public class GmapElementDto
    {
        public GmapDistanceDto distance { get; set; }
        public GmapDurationDto duration { get; set; }
        public GmapDurationDto duration_in_traffic { get; set; }
        public string status { get; set; }
    }
    public class GmapDistanceDto
    {
        public string text { get; set; }
        public int value { get; set; }
    }
    public class GmapDurationDto
    {
        public string text { get; set; }
        public int value { get; set; }
    }
}
