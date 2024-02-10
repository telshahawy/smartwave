using System;
using SW.HomeVisits.Application.Abstract.Enum;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class HomeVisitsWebApiResponse<T>
    {
        [JsonProperty("responseCode")]
        public WebApiResponseCodes ResponseCode  { get; set; }

        [JsonProperty("response")]
        public T Response { get; set; }

        [JsonProperty("message")]
        public string  Message { get; set; }

        [JsonProperty("errorList")]
        public List<string> ErrorList { get; set; }
    }
}
