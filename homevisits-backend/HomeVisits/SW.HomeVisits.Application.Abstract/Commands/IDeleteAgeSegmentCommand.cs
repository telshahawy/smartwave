using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IDeleteAgeSegmentCommand
    {
        Guid AgeSegmentId { get; set; }
    }
}
