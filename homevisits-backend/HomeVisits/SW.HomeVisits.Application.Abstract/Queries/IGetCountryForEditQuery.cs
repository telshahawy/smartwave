using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetCountryForEditQuery
    {
        public Guid CountryId { get; set; }
    }
}
