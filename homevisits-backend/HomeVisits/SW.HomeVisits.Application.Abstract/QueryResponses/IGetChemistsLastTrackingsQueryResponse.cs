using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface IGetChemistsLastTrackingsQueryResponse : IPaggingResponse
    {
        public List<ChemistLastTrackingLogDto> ChemistLastTrackingLogs { get; set; }
    }
}
