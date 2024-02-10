using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IDeleteReasonCommand
    {
        int ReasonId { get; set; }
    }
}
