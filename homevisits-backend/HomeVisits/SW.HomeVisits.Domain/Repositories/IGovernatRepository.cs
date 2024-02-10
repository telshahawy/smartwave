using System;
using System.Threading.Tasks;
using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Domain.Repositories
{
    public interface IGovernatRepository : IDisposableRepository
    {
        void PresistNewGovernate(Governate governate);
        int GetLatestGovernateCode();
        void UpdateGovernate(Governate governate);
        Governate GetGovernateById(Guid governateId);
        void DeleteGovernate(Guid governateId);
        bool GovernateNameExists(string name, Guid countryId, Guid governateId);
    }
}
