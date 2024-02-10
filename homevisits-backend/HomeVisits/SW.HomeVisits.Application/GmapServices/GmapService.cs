using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.GmapServices;
using SW.HomeVisits.Application.Abstract.Intergartions;

namespace SW.HomeVisits.Application.GmapServices
{
    public class GmapService : IGmapService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IIntegrationManager _integrationManager;
        public GmapService(IHttpContextAccessor httpContextAccessor,IIntegrationManager integrationManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            _integrationManager = integrationManager;
        }
        public async Task<GMapRoutingDto> GetDistanceMatrix(GmapRoutingInputsDto inputs)
        {
            string query = @"?origins=" + inputs.Origins + "&destinations=" + inputs.Destinations.Aggregate((i, j) => i + "|" + j) + "&key=AIzaSyALncxozdznk1qix-2VqVZbBAHzxkPOXIQ&units=imperial";
            string url = "https://maps.googleapis.com/maps/api/";
            string endPoint = @"distancematrix/json";
            var response = await _integrationManager.GetHttpResponse<GMapRoutingDto, bool?>(HttpVerb.Get,
                url,
                endPoint + query, null, false);
            return response.Response;
        }

    }
}