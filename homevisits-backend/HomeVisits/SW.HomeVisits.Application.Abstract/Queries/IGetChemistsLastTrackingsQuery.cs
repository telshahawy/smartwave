using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetChemistsLastTrackingsQuery : IPaggingQuery
    {
        public string Name { get; set; }
        CultureNames CultureName { get; set; }
    }
}
