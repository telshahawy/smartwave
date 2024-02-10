using System;
using System.Linq;
using System.Threading.Tasks;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using SW.HomeVisits.Infrastruture.Data;

namespace SW.HomeVisits.Infrastruture.Presistance.Repositories
{
    internal class CountryRepository : EfRepository<HomeVisitsDomainContext>, ICountryRepository
    {
        public CountryRepository(HomeVisitsDomainContext context) : base(context)
        {
        }

        public bool CountryNameExists(string name, Guid clientId, Guid countryId)
        {
            var result = false;
            if (countryId == Guid.Empty)
            {
                result = Context.Countries.Any(x => x.ClientId == clientId && x.CountryNameEn == name && x.IsActive == true && x.IsDeleted == false);
                return result;
            }
            result = Context.Countries.Any(x => x.ClientId == clientId && x.CountryNameEn == name && x.CountryId != countryId && x.IsActive == true && x.IsDeleted == false);
            return result;
        }

        public void DeleteCountry(Guid countryId)
        {
            var country = Context.Countries.Find(countryId);
            if (country == null)
            {
                throw new Exception("Country not found");
            }

            if (Context.Governates.Any(g => g.CountryId == countryId))
            {
                throw new Exception("Country is already having linked data!");
            }
            else
            {
                country.IsDeleted = true;
            }
            Context.Countries.Update(country);
        }

        public Country GetCountryById(Guid countryId)
        {
            return Context.Countries.SingleOrDefault(r => r.CountryId == countryId);
        }

        public int GetLatestCountryCode(Guid clientId)
        {
            var codes = Context.Countries.Where(x => x.ClientId == clientId);
            return !codes.Any() ? 0 : codes.Max(x => x.Code);
        }

        public void PresistNewCountry(Country country)
        {
            Context.Countries.Add(country);
        }

        public void UpdateCountry(Country country)
        {
            Context.Countries.Update(country);
        }
    }
}
