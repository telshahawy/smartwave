using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IDeleteClientuserCommand
    {
        Guid UserId { get; }
    }
}
