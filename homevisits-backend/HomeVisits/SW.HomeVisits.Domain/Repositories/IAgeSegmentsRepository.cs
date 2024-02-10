using System;
using System.Threading.Tasks;
using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Domain.Repositories
{
    public interface IAgeSegmentsRepository : IDisposableRepository
    {
        void DeleteAgeSegment(Guid ageSegmentId);
        void PresistNewAgeSegment(AgeSegment ageSegment);
        int GetLatestAgeSegmentCode(Guid clientId);
        void UpdateAgeSegment(AgeSegment ageSegment);
        AgeSegment GetAgeSegmentById(Guid ageSegmentId);
    }
}
