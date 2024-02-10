using System;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetAllAgeSegmentsQuery
    {
        public Guid ClientId { get; set; }
    }
}
