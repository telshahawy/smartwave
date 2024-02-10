using System;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchClientUserModel
    {
        public int? Code {get;set;}

        public string Name {get;set;}

        public string PhoneNumber {get;set;}

        public bool? IsActive {get;set;}

        public int? PageSize {get;set;}

        public int? CurrentPageIndex {get;set;}
    }
}
