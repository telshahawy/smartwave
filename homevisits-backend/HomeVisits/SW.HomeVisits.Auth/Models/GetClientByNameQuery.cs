using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.Auth.Models
{
    public class GetClientByNameQuery : IGetClientByNameQuery
    {
        public string Name { get; set; }
    }
}
