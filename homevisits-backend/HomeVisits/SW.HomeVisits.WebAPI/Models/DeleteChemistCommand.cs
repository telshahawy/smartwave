using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class DeleteChemistCommand : IDeleteChemistCommand
    {
        public Guid UserId { get; set; }
    }
}
