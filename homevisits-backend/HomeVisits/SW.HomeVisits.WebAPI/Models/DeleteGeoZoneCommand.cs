using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class DeleteGeoZoneCommand : IDeleteGeoZoneCommand
    {
        public Guid GeoZoneId { get; set; }
    }
}
