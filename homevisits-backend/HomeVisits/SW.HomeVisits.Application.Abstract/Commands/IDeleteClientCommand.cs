using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Commands
{
   public interface IDeleteClientCommand
    {
        Guid ClientId { get; set; }

    }
}
