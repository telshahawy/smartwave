using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IDeleteCountryCommand
    {
        Guid CountryId { get; set; }
    }
}
