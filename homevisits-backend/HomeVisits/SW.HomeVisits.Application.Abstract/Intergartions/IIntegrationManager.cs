using System;
using System.Threading.Tasks;

namespace SW.HomeVisits.Application.Abstract.Intergartions
{
    public interface IIntegrationManager
    {
        Task<IntegrationResponse<TOutput>> GetHttpResponse<TOutput, TInput>(HttpVerb verb, string url, string endPoint, TInput input, bool throwException = false);
    }
}
