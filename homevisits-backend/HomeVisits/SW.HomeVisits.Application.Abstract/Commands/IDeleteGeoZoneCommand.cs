using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IDeleteGeoZoneCommand
    {
        Guid GeoZoneId { get; set; }
    }
}
