using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class DeleteCountryCommand : IDeleteCountryCommand
    {
        public Guid CountryId { get; set; }
    }
}
