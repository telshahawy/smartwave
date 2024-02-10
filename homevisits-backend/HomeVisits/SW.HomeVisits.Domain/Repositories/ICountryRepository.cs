using System;
using System.Threading.Tasks;
using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Domain.Repositories
{
    public interface ICountryRepository : IDisposableRepository
    {
        void PresistNewCountry(Country country);
        int GetLatestCountryCode(Guid clientId);
        void UpdateCountry(Country country);
        Country GetCountryById(Guid countryId);
        void DeleteCountry(Guid countryId);
        bool CountryNameExists(string name, Guid clientId, Guid countryId);
    }
}
