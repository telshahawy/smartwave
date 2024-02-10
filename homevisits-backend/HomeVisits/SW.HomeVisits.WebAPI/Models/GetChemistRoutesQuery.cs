using System;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetChemistRoutesQuery : IGetChemistRoutesQuery
    {
        public Guid ChemistId { get; set; }

        public float? StartLatitude { get; set; }

        public float? StartLongitude { get; set; }

        public CultureNames CultureName { get; set; }

        public Guid ClientId { get; set; }
    }
}
