using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetAgeSegmentForEditQuery : IGetAgeSegmentForEditQuery
    {
        public Guid AgeSegmentId { get; set; }
    }
}
