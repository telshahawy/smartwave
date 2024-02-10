using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class DeleteClientUserCommand : IDeleteClientuserCommand
    {
        public Guid UserId { get; set; }
    }
}
