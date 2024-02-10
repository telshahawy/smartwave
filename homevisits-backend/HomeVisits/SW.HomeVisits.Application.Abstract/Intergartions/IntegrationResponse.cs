using System;
using System.Collections.Generic;
using System.Net;

namespace SW.HomeVisits.Application.Abstract.Intergartions
{
    public class ErrorItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Message { get; set; }
    }
    public class IntegrationResponse<T>
    {
        public bool IsSucceded { get; set; }
        public string Message { get; set; }
        public List<ErrorItem> Errors { get; set; }
        public T Response { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
