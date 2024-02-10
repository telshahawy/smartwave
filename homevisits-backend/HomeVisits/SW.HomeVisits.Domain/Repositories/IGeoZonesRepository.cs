using System;
using System.Threading.Tasks;
using SW.Framework.Domain;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Domain.Repositories
{
    public interface IGeoZonesRepository : IDisposableRepository
    {
        void DeleteGeoZone(Guid geoZoneId);
        void PresistNewGeoZone(GeoZone geoZone);
        int GetLatestGeoZoneCode();
        void UpdateGeoZone(GeoZone geoZone);
        GeoZone GetGeoZone(Guid id);
        void ChangeEntityStateToAdded<T>(T entity);
        void ChangeEntityStateToModified<T>(T entity);
        void ChangeEntityStateToDeleted<T>(T entity);
    }
}
