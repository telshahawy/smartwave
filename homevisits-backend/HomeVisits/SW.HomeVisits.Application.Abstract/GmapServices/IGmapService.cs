using System;
using System.Threading.Tasks;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Application.Abstract.GmapServices
{
    public interface IGmapService
    {
        Task<GMapRoutingDto> GetDistanceMatrix(GmapRoutingInputsDto inputs);
    }
}
