using SW.HomeVisits.Application.Abstract.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class DeleteClientCommand : IDeleteClientCommand
    {
        public Guid ClientId { get; set; }

    }
}
