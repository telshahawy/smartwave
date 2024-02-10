using System;
using System.Linq;
using System.Threading.Tasks;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using SW.HomeVisits.Infrastruture.Data;

namespace SW.HomeVisits.Infrastruture.Presistance.Repositories
{
    internal class GovernatRepository : EfRepository<HomeVisitsDomainContext>, IGovernatRepository
    {
        public GovernatRepository(HomeVisitsDomainContext context) : base(context)
        {
        }
        public void DeleteGovernate(Guid governateId)
        {
            var governate = Context.Governates.Find(governateId);
            if (governate == null)
            {
                throw new Exception("Governate not found");
            }

            if (Context.GeoZones.Any(g => g.GovernateId == governateId))
            {
                throw new Exception("Governate is already having linked data!");
            }
            else
            {
                governate.IsDeleted = true;
            }
            Context.Governates.Update(governate);
        }

        public Governate GetGovernateById(Guid governateId)
        {
            return Context.Governates.SingleOrDefault(r => r.GovernateId == governateId);
        }

        public int GetLatestGovernateCode()
        {
            var codes = Context.Governates;
            return !codes.Any() ? 0 : codes.Max(x => x.Code);
        }

        public bool GovernateNameExists(string name, Guid countryId, Guid governateId)
        {
            var result = Context.Governates.Any(x => x.CountryId == countryId && x.GoverNameEn == name && x.GovernateId != governateId);
            return result;
        }

        public void PresistNewGovernate(Governate governate)
        {
            Context.Governates.Add(governate);
        }

        public void UpdateGovernate(Governate governate)
        {
            Context.Governates.Update(governate);
        }
    }
}
