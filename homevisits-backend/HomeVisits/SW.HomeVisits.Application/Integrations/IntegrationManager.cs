using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using SW.HomeVisits.Application.Abstract.Intergartions;

namespace SW.HomeVisits.Application.Integrations
{
    public class IntegrationManager : IIntegrationManager
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public IntegrationManager(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
   }
        public async Task<IntegrationResponse<TOutput>> GetHttpResponse<TOutput, TInput>(HttpVerb verb, string url, string endPoint, TInput input, bool throwException = false)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StringValues authorizationHeaderStringValues;
                string authorizationHeader;
                httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationHeaderStringValues);
                authorizationHeader = authorizationHeaderStringValues.FirstOrDefault();
                if (!string.IsNullOrEmpty(authorizationHeader))
                {
                    var passedAuthorization = authorizationHeader.Trim().Split(' ');
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(passedAuthorization.First(), passedAuthorization.Last());
                }
                StringContent content = null;
                // HTTP POST
                HttpResponseMessage response = null;

                switch (verb)
                {
                    case HttpVerb.Get:
                        response = await client.GetAsync(endPoint);
                        break;
                    case HttpVerb.Post:
                        content = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");
                        response = await client.PostAsync(endPoint, content);
                        break;
                    case HttpVerb.Put:
                        content = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");
                        response = await client.PutAsync(endPoint, content);
                        break;
                    case HttpVerb.Delete:
                        response = await client.DeleteAsync(endPoint);
                        break;
                    default:
                        break;

                }

                //response.EnsureSuccessStatusCode();
                string data = await response.Content.ReadAsStringAsync();
                var result = new IntegrationResponse<TOutput>();
                result.IsSucceded = response.IsSuccessStatusCode;
                result.StatusCode = response.StatusCode;

                try
                {
                    var serializeOptions = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    result.Response = JsonSerializer.Deserialize<TOutput>(data, serializeOptions);
                }
                catch (Exception ex)
                {
                    //logger.Write(LogEventLevel.Error, ex, "");
                }

                if (!response.IsSuccessStatusCode)
                {
                    SerializableError err = null;
                    try
                    {
                        err = JsonSerializer.Deserialize<SerializableError>(data);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    if (err?.Any() == true)
                    {
                        var message = err.First().Value.ToString();
                        if (throwException)
                        {
                            var ex = new Exception(message);
                            foreach (var item in err)
                            {
                                if (!ex.Data.Contains(item.Key))
                                {
                                    ex.Data.Add(item.Key, item.Value);
                                }
                            }
                            throw ex;
                        }
                        else
                        {
                            result.Message = message;
                            result.Errors = new List<ErrorItem>();

                            foreach (var item in err)
                            {
                                if (result.Errors.All(x => x.Key != item.Key))
                                {
                                    result.Errors.Add(new ErrorItem
                                    {
                                        Key = item.Key,
                                        Value = Convert.ToString(item.Value)
                                    });
                                }
                            }
                        }
                    }
                }

                return result;
            }
        }
    }
}
