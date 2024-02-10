using System;
using System.Linq;
using System.Threading.Tasks;
using SW.Framework.EntityFrameworkCore;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using SW.HomeVisits.Infrastruture.Data;

namespace SW.HomeVisits.Infrastruture.Presistance.Repositories
{
    internal class GeoZonesRepository : EfRepository<HomeVisitsDomainContext>, IGeoZonesRepository
    {
        public GeoZonesRepository(HomeVisitsDomainContext context) : base(context)
        {
        }

        public void ChangeEntityStateToAdded<T>(T entity)
        {
            Context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }
        public void ChangeEntityStateToModified<T>(T entity)
        {
            Context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public void ChangeEntityStateToDeleted<T>(T entity)
        {
            Context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }
        public void DeleteGeoZone(Guid geoZoneId)
        {
            var geoZone = Context.GeoZones.Find(geoZoneId);
            if (geoZone == null)
            {
                throw new Exception("GeoZone not found");
            }

            if (Context.timeZoneFrame.Any(g => g.GeoZoneId == geoZoneId))
            {
                throw new Exception("GeoZone is already having linked data!");
            }
            else
            {
                geoZone.IsDeleted = true;
            }
            Context.GeoZones.Update(geoZone);
        }

        public GeoZone GetGeoZone(Guid id)
        {
            var geoZone = Context.GeoZones.Find(id);
            if(geoZone != null)
            {
                Context.Entry(geoZone).Collection(x => x.TimeZones).Load();
            }
            return geoZone;
        }

        public int GetLatestGeoZoneCode()
        {
            var codes = Context.GeoZones;
          
            return !codes.Any() ? 0 : codes.OrderByDescending(u => u.Code).FirstOrDefault().Code;
        }

        public void PresistNewGeoZone(GeoZone geoZone)
        {
            Context.GeoZones.Add(geoZone);
        }

        public void UpdateGeoZone(GeoZone geoZone)
        {
            Context.GeoZones.Update(geoZone);
        }
    }
}
