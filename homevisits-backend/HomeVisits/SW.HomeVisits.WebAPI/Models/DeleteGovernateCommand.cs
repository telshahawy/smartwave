using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class DeleteGovernateCommand : IDeleteGovernatCommand
    {
        public Guid GovernateId { get; set; }
    }
}
