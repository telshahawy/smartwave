using System;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetUserDeviceByChemistIdQuery
    {
        public Guid ChemistId { get; set; }
    }
}