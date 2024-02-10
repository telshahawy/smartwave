using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchClientUserQuery : ISearchClientUserQuery
    {
        public int? Code {get;set;}

        public string Name {get;set;}

        public string PhoneNumber {get;set;}

        public bool? IsActive {get;set;}

        public Guid ClientId {get;set;}

        public int? PageSize {get;set;}

        public int? CurrentPageIndex {get;set;}
    }
}
