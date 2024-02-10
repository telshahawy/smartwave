using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IDeleteChemistCommand
    {
        Guid UserId { get; set; }
    }
}
