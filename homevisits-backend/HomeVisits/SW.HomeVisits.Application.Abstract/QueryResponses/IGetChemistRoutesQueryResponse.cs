using System;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface IGetChemistRoutesQueryResponse
    {
        ChemistRouteDto Route { get; }
    }
}
