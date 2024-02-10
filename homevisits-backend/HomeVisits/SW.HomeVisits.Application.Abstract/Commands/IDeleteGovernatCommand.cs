using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IDeleteGovernatCommand
    {
        Guid GovernateId { get; set; }
    }
}
