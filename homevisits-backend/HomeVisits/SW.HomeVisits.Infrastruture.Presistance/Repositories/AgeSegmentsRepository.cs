using System;
using System.Linq;
using System.Threading.Tasks;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using SW.HomeVisits.Infrastruture.Data;

namespace SW.HomeVisits.Infrastruture.Presistance.Repositories
{
    internal class AgeSegmentsRepository : EfRepository<HomeVisitsDomainContext>, IAgeSegmentsRepository
    {
        public AgeSegmentsRepository(HomeVisitsDomainContext context) : base(context)
        {
        }

        public void DeleteAgeSegment(Guid ageSegmentId)
        {
            var ageSegment = Context.AgeSegments.Find(ageSegmentId);
            if (ageSegment == null)
            {
                throw new Exception("AgeSegment not found");
            }

            if (Context.Visits.Any(g => g.RelativeAgeSegmentId == ageSegmentId))
            {
                throw new Exception("AgeSegment is already having linked data!");
            }
            else
            {
                ageSegment.IsDeleted = true;
            }
            Context.AgeSegments.Update(ageSegment);
        }

        public AgeSegment GetAgeSegmentById(Guid ageSegmentId)
        {
            return Context.AgeSegments.SingleOrDefault(a => a.AgeSegmentId == ageSegmentId);
        }

        public int GetLatestAgeSegmentCode(Guid clientId)
        {
            var codes = Context.AgeSegments.Where(x => x.ClientId == clientId);
            return !codes.Any() ? 0 : codes.Max(x => x.Code);
        }

        public void PresistNewAgeSegment(AgeSegment ageSegment)
        {
            Context.AgeSegments.Add(ageSegment);
        }

        public void UpdateAgeSegment(AgeSegment ageSegment)
        {
            Context.AgeSegments.Update(ageSegment);
        }
    }
}
