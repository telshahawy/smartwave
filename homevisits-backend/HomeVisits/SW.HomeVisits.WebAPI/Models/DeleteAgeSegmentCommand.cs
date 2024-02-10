using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class DeleteAgeSegmentCommand : IDeleteAgeSegmentCommand
    {
        public Guid AgeSegmentId { get; set; }
    }
}
