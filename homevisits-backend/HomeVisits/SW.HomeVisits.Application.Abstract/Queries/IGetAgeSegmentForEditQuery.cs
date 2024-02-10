using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetAgeSegmentForEditQuery
    {
        public Guid AgeSegmentId { get; set; }
    }
}
