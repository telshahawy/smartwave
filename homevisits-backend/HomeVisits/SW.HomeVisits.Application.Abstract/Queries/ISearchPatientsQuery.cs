using System;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface ISearchPatientsQuery
    {
        public string PhoneNumber { get; set; }
        CultureNames CultureName { get; }
        Guid ClientId { get; }
    }
}
