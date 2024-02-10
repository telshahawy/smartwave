using SW.HomeVisits.Application.Abstract.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface IGetTimeZoneFramesQueryResponse
    {
        public List<TimeZoneFramesDto> TimeZoneFramesDto { get; set; }
    }
}
