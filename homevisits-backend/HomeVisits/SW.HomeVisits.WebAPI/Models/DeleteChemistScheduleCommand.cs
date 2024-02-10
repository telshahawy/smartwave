using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class DeleteChemistScheduleCommand : IDeleteChemistScheduleCommand
    {
        public Guid ChemistScheduleId { get; set; }

        public Guid ClientId { get; set; }
    }
}
