using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IResetUserPasswordCommand
    {
        Guid UserId { get; }
    }
}
