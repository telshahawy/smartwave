using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Models
{
    public class GetChemistRoutesQueryResponse : IGetChemistRoutesQueryResponse
    {
        public ChemistRouteDto Route { get; set; }
    }
}
